using Microsoft.AspNetCore.Mvc;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.API.Controllers
{
    [ApiController]
    [Route("countries")]
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
            return Ok(countries);
        }

        [HttpGet]
        [Route("/countries/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var country = await _countryService.GetById(id);
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
        public async Task<IActionResult> UpdateCountry([FromBody] Country country)
        {
            if (await _countryService.UpdateCountry(country))
                return Ok(country.Id);

            return BadRequest("Ocorreu um ero ao tentar alterar um país");
        }

    }
}
