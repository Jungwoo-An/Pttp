using System;
using System.Net;
using Pttp.Example.Controllers;
using Pttp.Server;

namespace Pttp.Example
{
    class Program
    {
        static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            using (var server = new HttpServer(IPAddress.Any, 8085))
            {
                server
                    .Logger(Log)
                    .Pipe("/", typeof(TestController))
                    .Start();

                Console.ReadLine();
            }
        }
    }
}
