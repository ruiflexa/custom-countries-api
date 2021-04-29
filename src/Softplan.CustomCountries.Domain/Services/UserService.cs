using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Extensions;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Domain.Services
{
    public class UserService : IUserService
    {

        private readonly List<User> _users;


        public UserService()
        {
            _users = new List<User>()
            {
                new User { Id = 1, UserName = "test", Password = "test" }
            };
        } 

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));

            if (user == null)
                return null;

            return user.WithoutPassword();
        }
    }
}
