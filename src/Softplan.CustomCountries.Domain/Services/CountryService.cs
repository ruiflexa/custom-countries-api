using AutoMapper;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Repository;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using Softplan.CustomCountries.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddCountry(CountryViewModel country)
        {
            
            return await _countryRepository.AddCountry(_mapper.Map<Country>(country));
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

        public async Task<bool> UpdateCountry(CountryViewModel country)
        {
            return await _countryRepository.UpdateCountry(_mapper.Map<Country>(country));
        }
    }
}
