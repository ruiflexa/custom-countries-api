using FluentAssertions;
using Newtonsoft.Json;
using Softplan.CustomCountries.API.Tests.IntegrationTests.Fixture;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Xunit;

namespace Softplan.CustomCountries.API.Tests.IntegrationTests.Scenarios
{
    public class CountriesControllerIntegrationTests : IClassFixture<BaseTestFixture>
    {
        private readonly HttpClient _client;

        public CountriesControllerIntegrationTests(BaseTestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CountriesController_GetCountries_RetornarComSucesso()
        {
            // Arrange
            var name = "Brazil";            

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"test:test")));
            var response = await _client.GetAsync($"api/countries/?name={name}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
