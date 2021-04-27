using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softplan.CustomCountries.Infra.CrossCutting.IoC;

namespace Softplan.CustomCountries.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Injector.RegisterDI(services, configuration);
        }
    }
}
