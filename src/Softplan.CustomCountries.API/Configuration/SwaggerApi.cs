using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;


namespace Softplan.CustomCountries.API.Configuration
{
    public static class SwaggerApi
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Custom Countries API",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Rui Flexa",
                        Email = "ruiflexa@gmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Softplan",
                        Url = new Uri(configuration.GetSection("SoftplanUrl").Value)
                    }
                });
            });
        }

        public static void AddSwaggerUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
        }
    }

    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
    }
}
