using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Softplan.CustomCountries.API.Tests.IntegrationTests.Fixture
{
    public sealed class BaseTestFixture : IDisposable
    {
        private readonly TestServer Server;

        public HttpClient Client;

        public BaseTestFixture()
        {
            var builder = new WebHostBuilder()
             .UseStartup<Startup>()
               .ConfigureAppConfiguration(config =>
               {
                   config.SetBasePath(System.IO.Directory.GetCurrentDirectory());
                   config.AddJsonFile("appsettings.IntegrationTests.json", reloadOnChange: true, optional: false);
                   config.Build();
               });

            Server = new TestServer(builder);

            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
