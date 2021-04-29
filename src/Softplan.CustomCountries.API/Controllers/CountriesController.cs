using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using System.Net;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.API.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] string name)
        {
            var countries = await _countryService.GetCountries(name);

            if (countries.Count == 0)
                return NoContent();

            return Ok(countries);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var country = await _countryService.GetById(id);
            return Ok(country);
        }

        [HttpGet]
        [Route("custom/{id}")]
        public IActionResult GetByCustomCountryId(long id)
        {
            var country = _countryService.GetByCustomCountryId(id);
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] Country country)
        {
            if (await _countryService.AddCountry(country))
                return Ok(country);

            return BadRequest("Ocorreu um erro ao tentar inserir um país");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCountry([FromBody] Country country, string id)
        {
            if (await _countryService.UpdateCountry(country))
                return Ok(id);

            return BadRequest("Ocorreu um ero ao tentar alterar um país");
        }

    }
}
