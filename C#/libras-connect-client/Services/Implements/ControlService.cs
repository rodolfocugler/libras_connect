using System;
using System.Collections.Generic;
using libras_connect_client.Views;
using libras_connect_client.Handler;
using libras_connect_domain.Services.Implements.Net;
using libras_connect_client.Services.Interfaces;
using libras_connect_domain.Services.Interfaces;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Models;
using libras_connect_domain.Enums;
using libras_connect_infrastructure.Serialize;
using System.Threading.Tasks;
using libras_connect_domain.DTO;
using System.Threading;

namespace libras_connect_client.Services.Implements
{
    /// <summary>
    /// Implementation of IControlService<see cref="IControlService"/>
    /// </summary>
    public class ControlService : IControlService, ISocketCallback, IObserver
    {
        private readonly ICntkService _cntkService;
        private readonly IBitmapService _bitmapService;
        private readonly ISignalRepository _signalRepository;

        private Stack<DataSocket> _imageQueue;
        private Stack<DataSocket> _dataQueue;
        private Queue<Signal> _signalQueue;

        private IHomePage _homePage;

        private ControlTypeEnum _controlTypeEnum;
        private string _word;
        private int _frameNumber;

        private System.Timers.Timer _timerSocket;
        private System.Timers.Timer _timerSignal;
        private System.Timers.Timer _timerClearStack;

        public ControlService(ISignalRepository signalRepository,
                              ICntkService cntkService,
                              IBitmapService bitmapService)
        {
            _signalRepository = signalRepository;
            _cntkService = cntkService;
            _bitmapService = bitmapService;

            int wokerThreads;
            int completionPortThreads;

            ThreadPool.GetMinThreads(out wokerThreads, out completionPortThreads);
            ThreadPool.SetMinThreads(20, completionPortThreads);

            _imageQueue = new Stack<DataSocket>();
            _dataQueue = new Stack<DataSocket>();
            _signalQueue = new Queue<Signal>();

            _timerSocket = new System.Timers.Timer(30);
            _timerSocket.AutoReset = false;
            _timerSocket.Enabled = true;
            _timerSocket.Elapsed += this.ProcessDataSocketQueue;
            _timerSocket.Start();

            _timerSignal = new System.Timers.Timer(30);
            _timerSignal.AutoReset = false;
            _timerSignal.Enabled = true;
            _timerSignal.Elapsed += this.ProcessSignalQueue;
            _timerSignal.Start();

            _timerClearStack = new System.Timers.Timer(60000);
            _timerClearStack.Elapsed += this.ClearQueue;
            _timerClearStack.Start();
        }

        /// <summary>
        ///     <para><see cref="ISocketCallback.Receive(string, CameraEnum)"/></para>
        /// </summary>
        public void Receive(string text, CameraEnum cameraEnum)
        {
            DataSocket dataSocket = null;

            try
            {
                dataSocket = SerializeUtil.Deserialize<DataSocket>(text);

                if (dataSocket != null)
                {
                    if (dataSocket.RealsenseType == RealsenseTypeEnum.HAND_DATA)
                    {
                        lock (_dataQueue)
                        {
                            _dataQueue.Push(dataSocket);
                        }
                    }
                    else if (dataSocket.RealsenseType == RealsenseTypeEnum.IMAGE_DATA)
                    {
                        lock (_imageQueue)
                        {
                            _imageQueue.Push(dataSocket);
                        }
                    }

                    Task.Run(() =>
                    {
                        if (_homePage != null)
                        {
                            _homePage.CallbackImage(dataSocket.Image, cameraEnum);
                            _homePage.CallbackData(dataSocket.HandData, cameraEnum);
                            _homePage.CallbackAlert(dataSocket.Alert, cameraEnum);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
            }
        }

        /// <summary>
        ///     <para><see cref="IControlService.SetControlType(ControlTypeEnum)"/></para>
        /// </summary>
        public void SetControlType(ControlTypeEnum controlTypeEnum)
        {
            this.ClearQueue();
            this._controlTypeEnum = controlTypeEnum;
        }

        /// <summary>
        ///     <para><see cref="IControlService.SetControlType(ControlTypeEnum, string)"/></para>
        /// </summary>
        public void SetControlType(ControlTypeEnum controlTypeEnum, string word)
        {
            if (controlTypeEnum == ControlTypeEnum.Saving)
            {
                _frameNumber = 0;
            }

            this.SetControlType(controlTypeEnum);

            this._word = word;
        }

        /// <summary>
        ///     <para><see cref="IControlService.SetHomePage(IHomePage)"/></para>
        /// </summary>
        public void SetHomePage(IHomePage homePage)
        {
            _homePage = homePage;
        }

        /// <summary>
        ///     <para><see cref="IControlService.GetControlType"/></para>
        /// </summary>
        public ControlTypeEnum GetControlType()
        {
            return _controlTypeEnum;
        }

        /// <summary>
        /// Process both queues of datasocket
        /// </summary>
        private void ProcessDataSocketQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            while (_dataQueue.Count > 0 && _imageQueue.Count > 0)
            {
                try
                {
                    Signal signal = new Signal();

                    Task dataTask = Task.Run(() =>
                    {
                        try
                        {
                            DataSocket data = null;

                            lock (_dataQueue)
                            {
                                data = _dataQueue.Pop();
                            }

                            signal.SetData(data);
                            signal.DataFloat = data.CntkInput;
                        }
                        catch
                        {

                        }
                    });

                    Task imageTask = Task.Run(() =>
                    {
                        try
                        {
                            DataSocket image = null;

                            lock (_imageQueue)
                            {
                                image = _imageQueue.Pop();
                            }

                            signal.SetImage(image);
                            signal.ImageFloat = image.CntkInput;
                        }
                        catch
                        {
                        }
                    });

                    dataTask.Wait();
                    imageTask.Wait();

                    _signalQueue.Enqueue(signal);
                }
                catch (Exception ex)
                {
                    ExceptionHandler eh = new ExceptionHandler(ex);
                }
            }

            _timerSocket.Start();
        }

        private void ProcessSignalQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            while (_signalQueue.Count > 0)
            {
                Signal signal = _signalQueue.Dequeue();

                switch (_controlTypeEnum)
                {
                    case ControlTypeEnum.Saving:
                        signal.Word = _word;
                        signal.FrameNumber = _frameNumber;
                        _frameNumber++;

                        _signalRepository.Create(signal);
                        break;

                    case ControlTypeEnum.Default:
                        string word = _cntkService.Compute(signal);

                        if (!String.IsNullOrWhiteSpace(word))
                        {
                            _homePage.PlotWord(word);
                        }
                        break;
                }
            }

            _timerSignal.Start();
        }

        private void ClearQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.ClearQueue();
        }

        /// <summary>
        /// Clear both queues
        /// </summary>
        private void ClearQueue()
        {
            try
            {
                lock (_dataQueue)
                {
                    _dataQueue.Clear();
                }

                lock (_imageQueue)
                {
                    _imageQueue.Clear();
                }

                lock (_signalQueue)
                {
                    _signalQueue.Clear();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify"/></para>
        /// </summary>
        public void Notify()
        {
            _timerSignal.Enabled = false;
            _timerSocket.Enabled = false;
        }
    }
}
