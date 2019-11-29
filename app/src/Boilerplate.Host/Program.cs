using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Host
{
    using System.IO;

    using Boilerplate.Host.Extensions;

    using Microsoft.AspNetCore;

    using Serilog;

    public class Program
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .AddEnvironmentVariables()
            .Build();

       
        public static int Main(string[] args)
        {
            var loggerConfig = new LoggerConfiguration().SetApplicationLogging(Configuration);
            Log.Logger = loggerConfig.CreateLogger();
            try
            {
                Log.Information("Getting the motors running...");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Configuration)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
