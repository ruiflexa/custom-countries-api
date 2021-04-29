using FluentAssertions;
using Newtonsoft.Json;
using Softplan.CustomCountries.API.Tests.IntegrationTests.Builders;
using Softplan.CustomCountries.API.Tests.IntegrationTests.Fixture;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        [Fact]
        public async Task CountriesController_GetCountries_RetornarVazio()
        {
            // Arrange
            var name = "xyz1";

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"test:test")));
            var response = await _client.GetAsync($"api/countries/?name={name}");

            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CountriesController_AddCountry_RetornarComSucesso()
        {
            // Arrange
            var country = new CountryTestBuilder().Default();

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"test:test")));


            var httpContent = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"api/countries", httpContent);

            // Assert            
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CountriesController_UpdateCountry_RetornarComSucesso()
        {
            // Arrange
            var country = new CountryTestBuilder().Default();

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"test:test")));

            var httpContent = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/countries/{country.Id}", httpContent);

            // Assert            
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CountriesController_UpdateCountry_RetornarComErro()
        {
            // Arrange
            var country = new CountryTestBuilder().Invalido();

            // Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"test:test")));

            var httpContent = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/countries/{country.Id}", httpContent);

            // Assert            
            response.StatusCode.Should().Match<HttpStatusCode>(s => s.Equals(HttpStatusCode.BadRequest) || s.Equals(HttpStatusCode.MethodNotAllowed) || s.Equals(HttpStatusCode.InternalServerError));
        }
    }
}
