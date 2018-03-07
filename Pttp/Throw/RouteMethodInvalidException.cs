using System;
using System.Collections.Generic;
using System.Text;

namespace Pttp.Throw
{
    public class RouteMethodInvalidException : Exception
    {
        public RouteMethodInvalidException(string @namespace) : base($"Route method invalid: {@namespace}") { }
    }
}
