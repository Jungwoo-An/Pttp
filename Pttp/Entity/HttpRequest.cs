using Pttp.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pttp.Entity
{
    public class HttpRequest
    {
        public Method Method { get; set; }
        public string RequestUrl { get; set; }
        public string RequestHttpVersion { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Content { get; set; }
    }
}
