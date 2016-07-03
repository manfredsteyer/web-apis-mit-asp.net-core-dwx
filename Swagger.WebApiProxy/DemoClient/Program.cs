using SwaggerServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClient
{
    class Program
    {


        static void Main(string[] args)
        {
            // Verwenden Sie hier Ihren Proxy ...

            var proxy = new FlightWebProxy(new Uri("http://localhost:37317"));

            var fluege = proxy.ApiFlightGet().Result;
            Console.WriteLine(fluege.Count);


            Console.WriteLine("Fertig!");
            Console.ReadLine();
            
        }
    }
}
