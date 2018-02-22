using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Pttp.Session
{
    public class HttpSession
    {
        private Socket _socket;

        public HttpSession(Socket socket)
        {
            _socket = socket;

            Init();
        }

        private void Init(int readable = 4096)
        {
            var buffer = new byte[readable];
            var readSize = _socket.Receive(buffer);

            var sb = new StringBuilder();

            sb.Append(buffer);

            while (readSize == 0 || _socket.Available > 0)
            {
                buffer = new byte[readable];
                readSize = _socket.Receive(buffer);

                sb.Append(buffer);
            }

#if DEBUG
            Debug.WriteLine(sb.ToString());
#endif
        }
    }
}
