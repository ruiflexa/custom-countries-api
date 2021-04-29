using FluentAssertions;
using Newtonsoft.Json;
using Softplan.CustomCountries.API.Tests.IntegrationTests.Fixture;
using Softplan.CustomCountries.API.ViewModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Softplan.CustomCountries.API.Tests.IntegrationTests.Scenarios
{
    public class AuthControllerIntegrationTests : IClassFixture<BaseTestFixture>
    {

        private readonly HttpClient _client;
        public AuthControllerIntegrationTests(BaseTestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task AuthController_Login_RetornarComSucesso()
        {

            // Arrange
            var user = new AuthViewModel()
            {
                UserName = "test",
                Password = "test"
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/auth/login", postContent);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AuthController_Login_RetornarComErro()
        {

            // Arrange
            var user = new AuthViewModel()
            {
                UserName = "test",
                Password = "error"
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/auth/login", postContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
