using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_client.Views
{
    /// <summary>
    /// Interface of HomePage
    /// </summary>
    public interface IHomePage
    {
        /// <summary>
        /// Set HandData Collection
        /// </summary>
        /// <param name="handsData">HandData Collection</param>
        /// <param name="cameraEnum">Camera Enum</param>
        void CallbackData(ICollection<HandData> handsData, CameraEnum cameraEnum);

        /// <summary>
        /// Set Alerts
        /// </summary>
        /// <param name="alert">Alert Collection</param>
        /// <param name="cameraEnum">Camera Enum</param>
        void CallbackAlert(ICollection<PXCMHandData.AlertType> alert, CameraEnum cameraEnum);

        /// <summary>
        /// Set Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="cameraEnum">Camera Enum</param>
        void CallbackMessage(string message, CameraEnum cameraEnum);

        /// <summary>
        /// Set Messages
        /// </summary>
        /// <param name="message">Message String</param>
        /// <param name="cameraEnum">Camera Enum</param>
        void SetMessage(string message, CameraEnum cameraEnum);

        /// <summary>
        /// Set Image from camera
        /// </summary>
        /// <param name="byteArray">Byte Array</param>
        /// <param name="cameraEnum">Camera Enum</param>
        void CallbackImage(byte[] byteArray, CameraEnum cameraEnum);

        /// <summary>
        /// Set word from cntk
        /// </summary>
        /// <param name="word">Word</param>
        void PlotWord(string word);
    }
}
