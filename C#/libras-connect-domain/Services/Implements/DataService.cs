using System.Collections.Generic;
using libras_connect_domain.Services.Interfaces;
using libras_connect_domain.Services.Implements.Net;
using libras_connect_domain.Models;
using libras_connect_domain.Enums;
using System;
using System.Drawing;
using libras_connect_infrastructure.Image;
using libras_connect_domain.Builder;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IDataService <see cref="IDataService"/>
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        ///     <para><see cref="IDataService.OnProcess(ICollection{HandData}, ICollection{PXCMHandData.AlertType})"/></para>
        /// </summary>
        public void OnProcess(ICollection<HandData> handData, ICollection<PXCMHandData.AlertType> alert)
        {
            SocketClient socketClient = SocketClient.GetInstance();

            DataSocket dataSocket = new DataSocket();
            dataSocket.Alert = alert;
            dataSocket.HandData = handData;
            dataSocket.CntkInput = CntkDataBuilder.Build(handData);
            dataSocket.DateTime = DateTime.UtcNow;

            socketClient.Send(dataSocket);
        }

        /// <summary>
        ///     <para><see cref="IDataService.OnProcess(Bitmap, Bitmap)"/></para>
        /// </summary>
        public void OnProcess(Bitmap bitmap, Bitmap rawBitmap)
        {
            SocketClient socketClient = SocketClient.GetInstance();

            DataSocket dataSocket = new DataSocket();
            dataSocket.CntkInput = CntkDataBuilder.Build(bitmap);
            dataSocket.Image = BitmapUtil.ImageToByte(rawBitmap);
            dataSocket.DateTime = DateTime.UtcNow;
            
            socketClient.Send(dataSocket);
        }
    }
}
