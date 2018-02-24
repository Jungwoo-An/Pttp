using System;
using System.Collections.Generic;
using System.Text;

namespace Pttp.Throw
{
    public class NotSupportedMethodException : Exception
    {
        public NotSupportedMethodException(string method) : base($"Not supported http method: {method}") { }
    }
}
