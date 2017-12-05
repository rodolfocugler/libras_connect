using libras_connect_domain.DTO;
using libras_connect_domain.Enums;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Implements.Net
{
    public class SocketServer : IObserver
    {
        private ISocketCallback _socketCallback;
        private ManualResetEvent _allDone;
        private Thread _thread;
        private CameraEnum _cameraEnum;

        class StateObject
        {
            public Socket workSocket = null;
            public const int BufferSize = 1048576; //1 megabyte
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }

        public SocketServer(ISocketCallback socketCallback, string ip, int port, CameraEnum cameraEnum)
        {
            _socketCallback = socketCallback;
            _cameraEnum = cameraEnum;
            _allDone = new ManualResetEvent(false);

            this.StartListening(ip, port);
        }

        /// <summary>
        /// Start server socket
        /// </summary>
        /// <param name="ip">Socket IP</param>
        /// <param name="port">Socket port</param>
        private void StartListening(string ip, int port)
        {
            _thread = new Thread(() =>
             {
                 IPAddress ipAddress = IPAddress.Parse(ip);
                 IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

                 Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                 try
                 {
                     listener.Bind(localEndPoint);
                     listener.Listen(100);

                     while (true)
                     {
                         _allDone.Reset();

                         listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                         _allDone.WaitOne();
                     }

                 }
                 catch (Exception)
                 {
                 }
             });

            _thread.Start();
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            _allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                if (state.sb[state.sb.Length - 1] == '}')
                {
                    _socketCallback.Receive(state.sb.ToString(), _cameraEnum);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify"/></para>
        /// </summary>
        public void Notify()
        {
            if (_allDone != null)
            {
                _allDone.Reset();
            }

            if (_thread != null)
            {
                _thread.Abort();
            }
        }
    }
}
