using System;
using System.Collections.Generic;
using System.Text;

namespace Pttp.Session
{
    /// <summary>
    /// 연결된 세션들을 관리하는 클래스입니다.
    /// </summary>
    public class HttpSessionPool
    {
        #region Variable
        private static HttpSessionPool _instance = null;
        private object _lockObj = new object();
        private List<HttpSession> _sessions = new List<HttpSession>(10);
        #endregion

        #region Property
        public List<HttpSession> Session
        {
            get => _sessions;
        }

        public static HttpSessionPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HttpSessionPool();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion
        
        private HttpSessionPool()
        {
        }
    }
}
