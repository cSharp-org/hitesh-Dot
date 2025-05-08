using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace Dummy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the configuration
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            // Configure Web API routes
            WebApiConfig.Register(config);

            // Create the server
            using (var server = new HttpSelfHostServer(config))
            {
                // Start the server
                server.OpenAsync().Wait();

                Console.WriteLine("Server is running on http://localhost:8080/");
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
            }
        }
    }
}
