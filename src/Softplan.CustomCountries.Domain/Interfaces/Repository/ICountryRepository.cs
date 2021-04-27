using Softplan.CustomCountries.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Interfaces.Repository
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountriesAsync(string name);
        Task<Country> GetByIdAsync(string id);
        Country GetByCustomCountryId(long id);
        Task<bool> AddCountry(Country country);
        Task<bool> UpdateCountry(Country country);
    }
}
