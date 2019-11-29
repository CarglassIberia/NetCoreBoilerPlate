
namespace Boilerplate.Host.Extensions
{
    using System;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIf(this IApplicationBuilder app, bool condition,
                                                Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            return condition ? action(app) : app;
        }

        public static IApplicationBuilder UseIfNot(this IApplicationBuilder app, bool condition,
                                                   Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            return condition ? app : action(app);
        }
    }
}
