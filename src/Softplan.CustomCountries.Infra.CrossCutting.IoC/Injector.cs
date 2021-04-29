using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Softplan.CustomCountries.Domain.Interfaces.Repository;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using Softplan.CustomCountries.Domain.Services;
using Softplan.CustomCountries.Infra.Data.Configuration;
using Softplan.CustomCountries.Infra.Data.Repository;
using System;
using System.IO;

namespace Softplan.CustomCountries.Infra.CrossCutting.IoC
{
    public static class Injector
    {
        public static void RegisterDI(IServiceCollection services, IConfiguration configuration)
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datastore.json");
            services.AddSingleton<IDataStore>(new DataStore(jsonFilePath, keyProperty: "_id", reloadBeforeGetCollection: true));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IGraphQLClient>(g => new GraphQLHttpClient(Environment.GetEnvironmentVariable("GRAPHCOUNTRIES_BASEURL"), new NewtonsoftJsonSerializer()));
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddTransient(typeof(OptionsMonitor<AuthenticationSchemeOptions>));
            services.Configure<Settings>(options =>
            {
                options.SoftplanUrl = configuration.GetSection("SoftplanUrl").Value;
                options.GraphCountries = new Graphcountries()
                {
                    BaseUrl = Environment.GetEnvironmentVariable("GRAPHCOUNTRIES_BASEURL"),  // configuration.GetSection("GraphCountries:BaseUrl").Value,
                    UserName = Environment.GetEnvironmentVariable("GRAPHCOUNTRIES_USERNAME"), // configuration.GetSection("GraphCountries:UserName").Value,
                    Password = Environment.GetEnvironmentVariable("GRAPHCOUNTRIES_PASSWORD") // configuration.GetSection("GraphCountries:Password").Value
                };
            });
        }
    }
}
