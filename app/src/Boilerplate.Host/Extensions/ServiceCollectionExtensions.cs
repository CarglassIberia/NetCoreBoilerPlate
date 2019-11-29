namespace Boilerplate.Host.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Swashbuckle.AspNetCore.Swagger;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
          //Register here your shit

            return services;
        }

        public static IServiceCollection AddDatabases(this IServiceCollection services)
        {
            //services.AddDbContext<YourContext>();
            return services;
        }


        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.DescribeAllParametersInCamelCase();
                setup.DescribeStringEnumsInCamelCase();
                setup.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "Boilerplate Backend",
                    Version = "v1"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                };

                setup.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                setup.AddSecurityRequirement(security);
            });

            return services;
        }
        
        public static IServiceCollection AddSettingsFromSection<T>(this IServiceCollection services,
            IConfiguration Config, string sectionKey) where T : class, new()
        {
            var mySettings = new T();
            Config.GetSection(sectionKey).Bind(mySettings);

            services.AddSingleton(mySettings);
            return services;
        }
    }
}