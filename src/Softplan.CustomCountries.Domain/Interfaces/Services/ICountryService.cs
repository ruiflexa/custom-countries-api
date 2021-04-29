using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Interfaces.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries(string name);
        Task<bool> AddCountry(CountryViewModel country);
        Task<bool> UpdateCountry(CountryViewModel country);
        Task<Country> GetById(string id);
        Country GetByCustomCountryId(long id);
    }
}
