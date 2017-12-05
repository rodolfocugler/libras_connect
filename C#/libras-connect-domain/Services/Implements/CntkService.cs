using CNTK;
using libras_connect_domain.DTO;
using libras_connect_domain.Models;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Services.Interfaces;
using libras_connect_infrastructure.Config;
using System.Collections.Generic;
using System.Linq;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of ICntkService<see cref="ICntkService"/>
    /// </summary>
    public class CntkService : ICntkService, IObserver
    {
        private readonly string _filepath = ConfigValues.Dnn_Path;

        private Function _function;
        private Variable _inputVar;
        private NDShape _shape;
        private Variable _outputVar;

        private readonly IWordDNNRepository _wordDNNRepository;
        private readonly IVoiceService _voiceService;

        private IDictionary<int, WordDNN> _wordDNNDictionary;
        private string _lastWord;

        public CntkService(IWordDNNRepository wordDNNRepository,
                           IVoiceService voiceService)
        {
            _voiceService = voiceService;
            _wordDNNRepository = wordDNNRepository;
            _wordDNNDictionary = new Dictionary<int, WordDNN>();

            foreach (WordDNN wordDNN in _wordDNNRepository.Get())
            {
                _wordDNNDictionary.Add(wordDNN.Index, wordDNN);
            }

            _function = Function.Load(_filepath, DeviceDescriptor.CPUDevice);
            _inputVar = _function.Arguments.Single();
            _shape = _inputVar.Shape;

            _outputVar = _function.Output;
        }

        /// <summary>
        ///     <para><see cref="ICntkService.Compute(Signal)"/></para>
        /// </summary>
        public string Compute(Signal signal)
        {
            try
            {
                IDictionary<Variable, Value> inputDataMap = new Dictionary<Variable, Value>();

                IList<float> input = signal.CntkInput;

                if (input.Count != 1448)
                {
                    return null;
                }

                var inputVal = Value.CreateBatch<float>(_shape, input, DeviceDescriptor.CPUDevice);
                inputDataMap.Add(_inputVar, inputVal);

                Dictionary<Variable, Value> outputDataMap = new Dictionary<Variable, Value>();
                outputDataMap.Add(_outputVar, null);

                _function.Evaluate(inputDataMap, outputDataMap, DeviceDescriptor.CPUDevice);

                Value outputVal = outputDataMap[_outputVar];
                var outputData = outputVal.GetDenseData<float>(_outputVar);

                int? index = this.GetMaximum(outputData.ElementAt(0));

                if (index == null)
                {
                    return null;
                }
                else
                {
                    string word = _wordDNNDictionary[index.Value].Word;

                    if (word != _lastWord)
                    {
                        //_voiceService.Speak(word);
                        return word;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get index of maximum value in a float list
        /// </summary>
        /// <param name="vector">float list</param>
        /// <returns>index of maximum value</returns>
        private int? GetMaximum(IList<float> vector)
        {
            int? max = null;

            for (int i = 0; i < vector.Count; i++)
            {
                float value = vector[i];

                if (value > ConfigValues.Cntk_Threshold && (max == null || vector[max.Value] < value))
                {
                    max = i;
                }
            }

            return max;
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify()"/></para>
        /// </summary>
        public void Notify()
        {
            if (_function != null)
            {
                _function.Dispose();
            }
        }
    }
}
