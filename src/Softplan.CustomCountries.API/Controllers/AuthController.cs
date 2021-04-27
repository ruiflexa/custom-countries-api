using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.CustomCountries.API.ViewModel;
using Softplan.CustomCountries.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("auth")]
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
