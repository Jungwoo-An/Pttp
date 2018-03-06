using Pttp.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Pttp.Route
{
    /// <summary>
    /// 라우팅 정보를 갖는 어트리뷰트로 사용 되는 클래스
    /// </summary>
    public class HttpRoute : Attribute
    {
        public string Path { get; set; }
        public Method Method { get; set; }
        internal MethodInfo Invoker { get; set; }

        public HttpRoute(string path, Method httpMethod)
        {
            this.Path = path;
            this.Method = httpMethod;
        }
    }
}
