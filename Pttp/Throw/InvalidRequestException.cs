using System;
using System.Collections.Generic;
using System.Text;

namespace Pttp.Throw
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base("Invalid http request") { }
    }
}
