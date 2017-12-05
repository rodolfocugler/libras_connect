using libras_connect_domain.DTO;
using libras_connect_domain.Handler;
using libras_connect_domain.Models;
using libras_connect_domain.Services.Interfaces;
using libras_connect_infrastructure.Image;
using libras_connect_infrastructure.Serialize;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IRealsenseService <see cref="IRealsenseService"/>
    /// </summary>
    public class RealsenseTestService : BaseRealsesenseService, IRealsenseService, IObserver
    {
        private int _frame;
        private IDictionary<string, int> _frameDic;
        private readonly IDataService _dataService;
        private Timer _timer;
        private IDictionary<string, List<string>> _lines;
        private bool _isRunning;

        public RealsenseTestService(IDataService dataService)
        {
            _dataService = dataService;
            _isRunning = true;
            _timer = new Timer(32);
            _timer.Elapsed += this.Start;
            _timer.AutoReset = false;
        }

        /// <summary>
        ///     <para><see cref="IRealsenseService.Start()"/></para>
        /// </summary>
        public void Start()
        {
            string controlFilepath = @"C:\Users\rodol\Documents\control.txt";

            List<string> control = File.ReadLines(controlFilepath).ToList();
            string option = control[control.Count - 1];

            int max = 0;

            if (option == "image")
            {
                option = "data";
                max = control.Count - 1;
            }
            else
            {
                option = "image";

                using (StreamWriter sw = File.AppendText(controlFilepath))
                {
                    sw.WriteLine(option);
                }

                max = control.Count;
            }

            _lines = new Dictionary<string, List<string>>();
            _frameDic = new Dictionary<string, int>();

            for (int i = 0; i < max; i++)
            {
                string filepath = String.Format(@"C:\users\rodol\documents\projetos\tcc\libras_connect test\{0}_{1}.txt", option, control[i]);

                _lines.Add(control[i], new List<string>());
                _frameDic.Add(control[i], 0);

                _lines[control[i]].AddRange(File.ReadLines(filepath).ToList());
            }

            _timer.Start();
        }

        private void Start(object sender, ElapsedEventArgs e)
        {
            try
            {
                KeyValuePair<string, List<string>> kvp = _lines.ElementAt(_frame / 500);

                string word = kvp.Key;
                string text = kvp.Value[_frameDic[word]];

                if ((_frame + 1) / 500 < _lines.Count)
                {
                    _frame++;
                }
                else
                {
                    _frame = 0;
                }

                if (_frameDic[word] + 1 < _lines[word].Count)
                {
                    _frameDic[word]++;
                }
                else
                {
                    _frameDic[word] = 0;
                }

                DataSocket dataSocket = SerializeUtil.Deserialize<DataSocket>(text);

                if (dataSocket.HandData != null)
                {
                    _dataService.OnProcess(dataSocket.HandData, new List<PXCMHandData.AlertType>());
                }
                else if (dataSocket.Image != null)
                {
                    Bitmap bitmap = BitmapUtil.ByteToImage(dataSocket.Image);
                    _dataService.OnProcess(bitmap, bitmap); //TODO: Update width, height and stride values to 32, 24 and 96 in BitmapUtil 
                }

                if (_isRunning)
                {
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
            }
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify()"/></para>
        /// </summary>
        public void Notify()
        {
            _isRunning = false;
        }
    }
}
