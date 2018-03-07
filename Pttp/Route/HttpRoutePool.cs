using Pttp.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Pttp.Route
{
    public class HttpRoutePool
    {
        #region Variable
        private static HttpRoutePool _instance = null;
        private static object _lockObj = new object();
        private Dictionary<string, HttpRoute> _routes = new Dictionary<string, HttpRoute>();
        #endregion

        #region Property
        public static HttpRoutePool Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HttpRoutePool();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region Method
        public void Add(string path, HttpRoute httpRoute)
        {
            _routes.Add(path, httpRoute);
        }

        public HttpRoute Get(HttpRequest req)
        {
            if (!_routes.ContainsKey(req.RequestUrl))
            {
                return null;
            }

            return _routes[req.RequestUrl];
        }
        #endregion

        private HttpRoutePool()
        {
        }
    }
}
