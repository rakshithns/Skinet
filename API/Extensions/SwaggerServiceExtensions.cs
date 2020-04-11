using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentationServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(o=>
            {
                o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Skinet API", Version = "v1"});
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(o=>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1");
            });
            return app;
        }
    }
}