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
        }
    }
}
