using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Softplan.CustomCountries.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("config")]
    public class ConfigController : ControllerBase
    {
        private const string GITHUB_ACESSO = "https://github.com/ruiflexa/custom-countries-api";

        [HttpGet]
        public string Get()
        {
            return GITHUB_ACESSO;
        }
    }

}
