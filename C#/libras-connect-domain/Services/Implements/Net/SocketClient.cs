using libras_connect_infrastructure.Serialize;
using libras_connect_infrastructure.Validation;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace libras_connect_domain.Services.Implements.Net
{
    public class SocketClient
    {
        private readonly IPEndPoint _ipEndPoint;
        private static SocketClient _socketClient;
        private Socket _socket;

        #region Constructors
        /// <summary>
        /// Get Instance of SocketClient
        /// </summary>
        /// <param name="ip">Server IP</param>
        /// <param name="port">Server Port</param>
        /// <returns>SocketClient</returns>
        public static SocketClient GetInstance(string ip, int port)
        {
            if (_socketClient == null)
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(ip);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                    _socketClient = new SocketClient(ipEndPoint);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return _socketClient;
        }

        public static SocketClient GetInstance()
        {
            if (_socketClient == null)
            {
                throw new ArgumentNullException("O socket deve ser construído primeiro");
            }

            return _socketClient;
        }

        /// <summary>
        /// Get Instance of SocketClient
        /// </summary>
        /// <param name="address">Server Address</param>
        /// <returns>SocketClient</returns>
        public static SocketClient GetInstance(string address)
        {
            if (_socketClient == null)
            {
                ValidationUtil.ValidIPAddress(address);

                try
                {
                    string[] a = address.Split(':');

                    string ip = a[0];
                    int port = Convert.ToInt32(a[1]);

                    IPAddress ipAddress = IPAddress.Parse(ip);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                    _socketClient = new SocketClient(ipEndPoint);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return _socketClient;
        }

        private SocketClient(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
        }
        #endregion

        /// <summary>
        /// Send an object by Socket
        /// </summary>
        /// <param name="object">Any object</param>
        public void Send(object obj)
        {
            string text = SerializeUtil.Serialize(obj);

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(_ipEndPoint);

                byte[] msg = Encoding.ASCII.GetBytes(text);

                int bytesSent = _socket.Send(msg);
            }
            catch (Exception)
            {

            }
            finally
            {
                if (_socket != null && _socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
            }
        }
    }
}