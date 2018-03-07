using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Pttp.Entity
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ContentType { get; set; }
    }
}
