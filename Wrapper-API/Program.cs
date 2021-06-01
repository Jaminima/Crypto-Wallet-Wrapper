using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using GRLC_Wallet_Wrapper;

namespace Wrapper_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ensuring the Network is Running, this may take some time.\n");

            Cli_Manager.Start(true);

            Console.WriteLine("Network Started Successfully.\n");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}