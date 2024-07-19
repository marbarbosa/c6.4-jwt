using Escola.Attributes;
using Escola.Data;
using Escola.Extensions;
using Escola.Services;
using Escola.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Escola.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("")]
    public class AccountController : ControllerBase
    {
        // [AllowAnonymous] -- declara esse parametro quando o Authorize é declarado para a classe inteira
        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromServices] TokenService tokenService)
        { 
            var token = tokenService.GenerateToken(null);
            return Ok(token);
        }

        [Authorize(Roles ="user")]
        [HttpGet("v1/user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

        [Authorize(Roles = "author")]
        [HttpGet("v1/author")]
        public IActionResult GetAuthor() => Ok(User.Identity.Name);

        [Authorize(Roles = "admin")]
        [HttpGet("v1/admin")]
        public IActionResult GetAdmin() => Ok(User.Identity.Name);

        [ApiKey]
        [HttpGet("v1/TesteApiKey")]
        public IActionResult Get() => Ok();
    }
}

