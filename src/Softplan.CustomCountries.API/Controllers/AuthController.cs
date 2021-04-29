using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using Softplan.CustomCountries.Domain.ViewModel;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthViewModel model)
        {
            var user = await _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Usuário/Senha inválidos!" });

            return Ok(user);
        }
    }
}
