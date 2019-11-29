using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Host
{
    using Boilerplate.Host.Extensions;
    using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public IConfiguration Config { get; }
        public ILogger Log;


        public void ConfigureServices(IServiceCollection services)
        {
            API.Module.ConfigureServices(services)
                .AddOpenApi()
                .AddCors(
                options =>
                    {
                        options.AddPolicy(
                            "DevPolicyCors",
                            builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
                    });
              
            services.AddMvc()
                .AddApplicationPart(API.Module.GetApiAssembly())
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
            API.Module.Configure(
                app,
                host => host 
                    .UseIfNot(env.IsDevelopment(), appBuilder => appBuilder.UseHsts())
                    //.UseIf(env.IsDevelopment(), appBuilder =>
                    //    appBuilder.EnsureDatabaseIsSeeded<PayrollContext>(true, DataSeedTools.SeedDataWithFaker))
                    .UseAuthentication()
                    .UseSwagger()
                    .UseCors("DevPolicyCors")
                    .UseSwaggerUI(setup => { setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Sage PoC"); })
            );

        }
    }
}
    
