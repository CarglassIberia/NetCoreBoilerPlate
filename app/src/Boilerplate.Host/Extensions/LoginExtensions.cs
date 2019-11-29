using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Host.Extensions
{
    using Microsoft.Extensions.Configuration;

    using Serilog;
    using Serilog.Events;

    public static class LoggingExtensions
    {
        public static LoggerConfiguration SetApplicationLogging(
            this LoggerConfiguration loggerConfig,
            IConfiguration Configuration)
        {

            loggerConfig.MinimumLevel.Debug().MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich
                .FromLogContext().WriteTo.Console();


            return loggerConfig;
        }
    }
}
