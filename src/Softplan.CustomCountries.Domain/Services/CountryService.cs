using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Repository;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<bool> AddCountry(Country country)
        {
            return await _countryRepository.AddCountry(country);
        }

        public async Task<Country> GetById(string id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public Country GetByCustomCountryId(long id)
        {
            return _countryRepository.GetByCustomCountryId(id);
        }

        public async Task<List<Country>> GetCountries(string name)
        {
            return await _countryRepository.GetCountriesAsync(name);
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            return await _countryRepository.UpdateCountry(country);
        }
    }
}
