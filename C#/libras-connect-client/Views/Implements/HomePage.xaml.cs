using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Text;
using System.Windows.Threading;
using System.IO;
using libras_connect_client.Handler;
using libras_connect_client.Services.Interfaces;
using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using libras_connect_infrastructure.Image;

namespace libras_connect_client.Views.Implements
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page, IHomePage
    {
        private IControlService _controlService;
        private IBitmapService _bitmapService;
        private DispatcherOperation _dataDispatcherOperation;
        private DispatcherOperation _imageDispatcherOperation;

        public HomePage(IControlService controlService,
                        IBitmapService bitmapService)
        {
            _controlService = controlService;
            _controlService.SetHomePage(this);
            _bitmapService = bitmapService;

            InitializeComponent();
        }

        /// <summary>
        ///     <para><see cref="IHomePage.SetWord(string)"/></para>
        /// </summary>
        public void PlotWord(string word)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                lbl_word.Content = word;
            }));
        }

        /// <summary>
        /// Event when save button is clicked
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Event</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_controlService.GetControlType() != ControlTypeEnum.Saving)
            {
                if (String.IsNullOrWhiteSpace(tbx_word.Text))
                {
                    MessageBox.Show("Preencha a palavra", "Erro", MessageBoxButton.OK);
                }
                else
                {
                    _controlService.SetControlType(ControlTypeEnum.Saving, tbx_word.Text);
                    (sender as Button).Content = "Parar";
                }
            }
            else
            {
                _controlService.SetControlType(ControlTypeEnum.Default);
                (sender as Button).Content = "Salvar";
            }
        }

        /// <summary>
        ///     <para><see cref="IHomePage.CallbackData(ICollection{HandData}, CameraEnum)"/></para>
        /// </summary>
        public void CallbackData(ICollection<HandData> handsData, CameraEnum cameraEnum)
        {
            if (this.RunDispatcherOperation(_dataDispatcherOperation, handsData))
            {
                _dataDispatcherOperation = Dispatcher.InvokeAsync(new Action(() =>
                {
                    try
                    {
                        Bitmap image = _bitmapService.PaintImageCamera(handsData);
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                        switch (cameraEnum)
                        {
                            case CameraEnum.CAMERA_1:
                                image_cam_1.Source = _bitmapService.PaintImage(image);
                                break;

                            case CameraEnum.CAMERA_2:
                                image_cam_2.Source = _bitmapService.PaintImage(image);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
                    }
                }));
            }
        }

        /// <summary>
        ///     <para><see cref="IHomePage.CallbackMessage(string, CameraEnum)"/></para>
        /// </summary>
        public void CallbackMessage(string message, CameraEnum cameraEnum)
        {
            Dispatcher.InvokeAsync(new Action(() =>
            {
                this.SetMessage(message, cameraEnum);
            }));
        }

        /// <summary>
        ///     <para><see cref="IHomePage.CallbackAlert(ICollection{PXCMHandData.AlertType}, CameraEnum)"/></para>
        /// </summary>
        public void CallbackAlert(ICollection<PXCMHandData.AlertType> alert, CameraEnum cameraEnum)
        {
            Dispatcher.InvokeAsync(new Action(() =>
            {
                StringBuilder sb = new StringBuilder();

                if (alert != null)
                {
                    foreach (PXCMHandData.AlertType label in alert)
                    {
                        switch (label)
                        {
                            case PXCMHandData.AlertType.ALERT_HAND_CALIBRATED:
                                sb.Append("Câmera calibrada, ");
                                break;

                            case PXCMHandData.AlertType.ALERT_HAND_NOT_CALIBRATED:
                                sb.Append("Câmera não calibrada, ");
                                break;

                            case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BORDERS:
                                sb.Append("Mão(s) fora do limite da câmera, ");
                                break;

                            case PXCMHandData.AlertType.ALERT_HAND_TOO_CLOSE:
                                sb.Append("Mão(s) está(ão) muito perto da câmera, ");
                                break;

                            case PXCMHandData.AlertType.ALERT_HAND_TOO_FAR:
                                sb.Append("Mão(s) está(ao) muito longe da câmera, ");
                                break;

                            case PXCMHandData.AlertType.ALERT_HAND_LOW_CONFIDENCE:
                                sb.Append("Confiança da câmera está baixa, ");
                                break;
                        }
                    }
                }

                string message = sb.ToString();

                if (!String.IsNullOrWhiteSpace(message))
                {
                    message = message.Substring(0, message.Length - 2);

                    this.SetMessage(message, cameraEnum);
                }
            }));
        }

        /// <summary>
        ///     <para><see cref="IHomePage.SetMessage(string, CameraEnum)"/></para>
        /// </summary>
        public void SetMessage(string message, CameraEnum cameraEnum)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                switch (cameraEnum)
                {
                    case CameraEnum.CAMERA_1:
                        message_cam_1.Text = message;
                        break;

                    case CameraEnum.CAMERA_2:
                        message_cam_2.Text = message;
                        break;
                }
            }));
        }

        /// <summary>
        ///     <para><see cref="IHomePage.CallbackImage(byte[], CameraEnum)"/></para>
        /// </summary>
        public void CallbackImage(byte[] byteArray, CameraEnum cameraEnum)
        {
            if (this.RunDispatcherOperation(_imageDispatcherOperation, byteArray))
            {
                _imageDispatcherOperation = Dispatcher.InvokeAsync(new Action(() =>
                {
                    try
                    {
                        Bitmap bitmap = BitmapUtil.ByteToImage(byteArray);

                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);

                        switch (cameraEnum)
                        {
                            case CameraEnum.CAMERA_1:
                                image_cam_1.Source = _bitmapService.PaintImage(bitmap);
                                break;

                            case CameraEnum.CAMERA_2:
                                image_cam_2.Source = _bitmapService.PaintImage(bitmap);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
                    }
                }));
            }
        }

        /// <summary>
        /// Check if the DispatcherOperation must be run now
        /// </summary>
        /// <param name="dispatcherOperation">DispatcherOperation</param>
        /// <param name="obj">object that will be show</param>
        /// <returns></returns>
        private bool RunDispatcherOperation(DispatcherOperation dispatcherOperation, object obj)
        {
            return obj != null &&
                (_imageDispatcherOperation == null ||
                _imageDispatcherOperation.Status != DispatcherOperationStatus.Executing);
        }
    }
}
