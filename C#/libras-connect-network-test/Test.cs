using libras_connect_domain.Builder;
using libras_connect_domain.Models;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Services.Interfaces;
using libras_connect_infrastructure.Config;
using libras_connect_infrastructure.Image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_network_test
{
    public class Test : ITest
    {
        private readonly ICntkService _cntkService;
        private readonly ISignalRepository _signalRepository;
        private readonly IWordDNNRepository _wordDNNRepository;

        public Test(ICntkService cntkService,
                    ISignalRepository signalRepository,
                    IWordDNNRepository wordDNNRepository)
        {
            _signalRepository = signalRepository;
            _cntkService = cntkService;
            _wordDNNRepository = wordDNNRepository;
        }

        private IDictionary<string, List<List<Signal>>> GetDictionary()
        {
            ICollection<Signal> signalList = _signalRepository.Get();
            IDictionary<string, List<List<Signal>>> dictionary = new Dictionary<string, List<List<Signal>>>();

            foreach (Signal s in signalList)
            {
                if (!dictionary.ContainsKey(s.Word))
                {
                    dictionary.Add(s.Word, new List<List<Signal>>());
                }

                if (s.FrameNumber == 0)
                {
                    dictionary[s.Word].Add(new List<Signal>());
                }

                dictionary[s.Word].Last().Add(s);
            }

            return dictionary;
        }

        public void Compute(double minPercent, double maxPercent)
        {
            IDictionary<string, List<List<Signal>>> dictionary = this.GetDictionary();

            int count = 0;
            int error = 0;
            int correct = 0;

            Dictionary<string, Dictionary<string, int>> d = new Dictionary<string, Dictionary<string, int>>();

            foreach (KeyValuePair<string, List<List<Signal>>> kvp in dictionary)
            {
                string label = kvp.Key;
                List<List<Signal>> list = kvp.Value;

                int min = (int)Math.Round(list.Count * minPercent, 0);
                int max = (int)Math.Round(list.Count * maxPercent, 0);

                for (int i = min; i < max; i++)
                {
                    foreach (Signal signal in list[i])
                    {
                        count++;

                        Bitmap bitmap = BitmapUtil.ByteToImage(signal.Image);
                        signal.ImageFloat = CntkDataBuilder.Build(bitmap);

                        signal.DataFloat = CntkDataBuilder.Build(signal.HandData);

                        string result = _cntkService.Compute(signal);

                        if (result != null)
                        {
                            if (result != label)
                            {
                                if (!d.ContainsKey(label))
                                {
                                    d[label] = new Dictionary<string, int>();
                                }

                                if (!d[label].ContainsKey(result))
                                {
                                    d[label][result] = 0;
                                }

                                d[label][result] = d[label][result] + 1;

                                error++;
                            }
                            else
                            {
                                correct++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Min: {0}", minPercent);
            Console.WriteLine("Max: {0}", maxPercent);
            Console.WriteLine("Count: {0}", count);
            Console.WriteLine("Error: {0}", error);
            Console.WriteLine("Correct: {0}", correct);
        }

        public void BuildFileTrain()
        {
            IDictionary<string, List<List<Signal>>> dictionary = this.GetDictionary();

            using (StreamWriter sw = File.AppendText(ConfigValues.Train_Txt))
            {
                this.BuildFileTrain(sw, dictionary, 0, 0, 0.7);
            }

            using (StreamWriter sw = File.AppendText(ConfigValues.Test_Txt))
            {
                this.BuildFileTrain(sw, dictionary, 0, 0.7, 1);
            }

            Console.WriteLine("Finish");
        }

        private void BuildFileTrain(StreamWriter sw, IDictionary<string, List<List<Signal>>> dictionary, int level, double startPercent, double finishPercent)
        {
            //database
            ICollection<WordDNN> wordDNNList = _wordDNNRepository.Get();
            foreach (WordDNN wordDNN in wordDNNList)
            {
                _wordDNNRepository.Delete(wordDNN.Id);
            }

            Dictionary<string, int> neuronLabel = new Dictionary<string, int>();

            foreach (KeyValuePair<string, List<List<Signal>>> kvp in dictionary)
            {
                string label = kvp.Key;
                neuronLabel[label] = neuronLabel.Count;

                WordDNN wordDNN = new WordDNN();
                wordDNN.Index = neuronLabel[label];
                wordDNN.Word = label;

                _wordDNNRepository.Create(wordDNN);
            }

            string[] neuron = new string[neuronLabel.Count];

            for (int i = 0; i < neuronLabel.Count; i++)
            {
                neuron[i] = this.BuildLabel(i, neuronLabel.Count);
            };

            // Label
            foreach (KeyValuePair<string, List<List<Signal>>> kvp in dictionary)
            {
                string label = kvp.Key;
                List<List<Signal>> list = kvp.Value; // Sequence List

                for (int i = (int)(startPercent * list.Count); i < (double)list.Count * finishPercent; i++)
                {
                    List<Signal> listSignal = list[i]; // Sequence Frame

                    foreach (Signal s in listSignal)
                    {
                        StringBuilder sb = this.GetText(s, neuronLabel, neuron);
                        sw.WriteLine(sb.ToString());
                    }
                }
            }
        }

        private StringBuilder GetText(Signal s, IDictionary<string, int> neuronLabel, string[] neuron)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("|data");

            s.Bitmap = BitmapUtil.ByteToImage(s.Image);
            s.ImageFloat = CntkDataBuilder.Build(s.Bitmap);
            s.DataFloat = CntkDataBuilder.Build(s.HandData);

            foreach (float f in s.CntkInput)
            {
                sb.AppendFormat(string.Format(" {0}", f).Replace(",", "."));
            }

            sb.AppendFormat(" |label {0}", neuron[neuronLabel[s.Word]]);

            return sb;
        }

        private string BuildLabel(int pos, int len)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                if (i == pos)
                {
                    sb.Append("1 ");
                }
                else
                {
                    sb.Append("0 ");
                }
            }

            return sb.ToString();
        }
    }
}