using Softplan.CustomCountries.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Interfaces.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries(string name);
        Task<bool> AddCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<Country> GetById(string id);
        Country GetByCustomCountryId(long id);
    }
}
