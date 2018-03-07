using Pttp.Entity;
using Pttp.Enums;
using Pttp.Route;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pttp.Example.Controllers
{
    public static class TestController
    {
        [HttpRoute("/", Method.GET)]
        public static string doGet(HttpRequest req, HttpResponse res)
        {
            return "Hello World";
        }
    }
}
