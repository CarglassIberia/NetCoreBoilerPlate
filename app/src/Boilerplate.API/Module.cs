namespace Boilerplate.API
{
    using System;
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Newtonsoft.Json.Serialization;

    public static class Module
    {
      
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddApiExplorer()
                .AddDataAnnotations()
                .Services;
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            return configureHost(app)
                .UseMvc(routes =>
                    {
                        routes.MapRoute("default", "{controller=Default}/{action=Swagger}");
                    });
        }

        public static Assembly GetApiAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
