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
        private Dictionary<string, MethodInfo> _routes = new Dictionary<string, MethodInfo>();
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

        private HttpRoutePool()
        {
        }
    }
}
