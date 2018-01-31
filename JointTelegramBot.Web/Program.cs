using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace JointTelegramBot.Web
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.RollingFile(Directory.GetCurrentDirectory() + "/logs/CryptoGramBot.log")
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();


                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
                configurationBuilder.AddJsonFile("appsettings.json", false, true);

                var config = configurationBuilder.Build();

                Log.Logger = new LoggerConfiguration()
                   .WriteTo.RollingFile(Directory.GetCurrentDirectory() + "/logs/CryptoGramBot.log")
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                   .Enrich.FromLogContext()
                   .WriteTo.Console()
                   .CreateLogger();

                var serverUrls = config.GetSection("General").GetValue<string>("ServerUrls");
                var webHost = new WebHostBuilder()
                        .UseKestrel()
                        .UseLibuv(x => x.ThreadCount = 4)
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .UseConfiguration(config)
                        .UseSerilog()
                        .UseUrls(serverUrls)
                        .Build();

                webHost.Run();
                return 0;
            }
            catch (Exception)
            {

                return 1;
            }
           

        }
        //public static IWebHost BuildWebHost(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //       .UseStartup<Startup>()
        //       .UseUrls("ServerUrls")
        //       .Build();

    }
}
