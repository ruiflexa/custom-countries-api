using Softplan.CustomCountries.Domain.Entities;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
