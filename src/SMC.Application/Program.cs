using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SMC
{
    public class Program
    {
        
        public static string url = "";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            var config = new ConfigurationBuilder().AddEnvironmentVariables("").Build();
            url = config["ASPNETCORE_URLS"] ?? "https://*:8080";
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>();
                    webBuilder.UseKestrel(opts =>
                    {
                        // Bind directly to a socket handle or Unix socket
                        // opts.ListenHandle(123554);
                        // opts.ListenUnixSocket("/tmp/kestrel-test.sock");
                        opts.Listen(IPAddress.Loopback, port: 5002);
                        opts.ListenAnyIP(8080);
                        opts.ListenLocalhost(8080, opts => opts.UseHttps());
                    });

                });


        


    }
}
