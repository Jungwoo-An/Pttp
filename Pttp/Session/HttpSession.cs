using Pttp.Entity;
using Pttp.Route;
using Pttp.Server;
using Pttp.Throw;
using Pttp.Util;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Pttp.Session
{
    public class HttpSession
    {
        private HttpServer _server;
        private Socket _socket;

        public HttpSession(HttpServer server, Socket socket)
        {
            _server = server;
            _socket = socket;
        }

        public void Init(int readable = 4096)
        {
            var buffer = new byte[readable];
            var readSize = _socket.Receive(buffer);

            var sb = new StringBuilder();

            sb.Append(Encoding.UTF8.GetString(buffer, 0, readSize));

            while (readSize == 0 || _socket.Available > 0)
            {
                buffer = new byte[readable];
                readSize = _socket.Receive(buffer);

                sb.Append(Encoding.UTF8.GetString(buffer, 0, readSize));
            }

            var req = Helper.ParseRequest(sb.ToString());
            _server.Log?.Invoke($"Request {req.RequestHttpVersion}: {req.RequestUrl} [{req.Method}]");
            _server.Log?.Invoke($"Content: {req.Content}");

            this.Handle(req);
        }

        private void Handle(HttpRequest req)
        {
            var res = new HttpResponse();

            var route = HttpRoutePool.Instance.Get(req);
            object content = null;
            if (route == null)
            {
                // 404 NOT FOUND
                res.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                try
                {
                    content = route.Invoker.Invoke(null, new object[] { req, res });
                }
                catch (TargetParameterCountException ex)
                {
                    throw new RouteMethodInvalidException(route.Invoker.Name);
                }
            }

            this.Response(res, content);
        }

        private void Response(HttpResponse res, object content)
        {
            _server.Log?.Invoke($"Response [{res.StatusCode}]: {content?.ToString()}");
        }
    }
}
