using System;
using System.Net;
using System.Net.Sockets;

namespace Pttp
{
    /// <summary>
    /// Http 서버 구현체
    /// </summary>
    public class HttpServer : IDisposable
    {
        #region Variable
        private Socket _socket;
        private IPEndPoint _host;
        private int _backlog = 100;
        #endregion

        #region Property
        /// <summary>
        /// 바인딩 될 Ip 주소입니다.
        /// </summary>
        public IPAddress Ip
        {
            get => _host?.Address;
        }

        /// <summary>
        /// 바인딩 될 포트입니다. (기본 값: 0)
        /// </summary>
        public int Port
        {
            get => _host?.Port ?? 0;
        }

        /// <summary>
        /// 원본 Socket 객체입니다.
        /// </summary>
        public Socket OriginalSocket
        {
            get => _socket;
        }
        #endregion

        /// <summary>
        /// ip 와 port 를 기반으로 소켓 객체를 생성합니다.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public HttpServer(IPAddress ip, int port = 80)
        {
            _host = new IPEndPoint(ip, port);
            _socket = new Socket(_host.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Socket 의 Backlog 값을 설정합니다.
        /// </summary>
        /// <param name="backlog"></param>
        public HttpServer Backlog(int backlog)
        {
            _backlog = backlog;

            return this;
        }

        public void Start()
        {
            if (_socket == null)
            {
                throw new ArgumentException("HttpServer 소켓이 정의되지 않았습니다.");
            }

            try
            {
                // 소켓 시작
                _socket.Bind(_host);
                _socket.Listen(100);

                while (true)
                {
                    _socket.Accept();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Http 서버를 종료합니다.
        /// </summary>
        public void Dispose()
        {
            if (_socket != null)
            {
                _socket.Dispose();
            }
        }
    }
}
