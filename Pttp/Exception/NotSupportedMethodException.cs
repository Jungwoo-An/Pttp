using System;
using System.Collections.Generic;
using System.Text;

namespace Pttp.Exception
{
    public class NotSupportedMethodException : System.Exception
    {
        public NotSupportedMethodException(string method) : base($"Not supported http method: {method}") { }
    }
}
