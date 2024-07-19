using Escola.Data;
using Escola.Extensions;
using Escola.Models;
using Escola.Services;
using Escola.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Escola.Controllers
{
    [ApiController]
    [Route("")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("v1/usuarios")]
        public async Task<IActionResult> Post(
            [FromBody] RegistrarViewModel model,
            [FromServices] EscolaDataContext context
                ) 
        {
            if (ModelState.IsValid == false) 
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            // tratar email duplicado

            // gerar usuário e senha
            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Email.Substring(0, model.Email.IndexOf("@"))
            };

            var senha = PasswordGenerator.Generate(16,false,false);
            usuario.Senha = senha; //PasswordHasher.Hash(senha); -- para salvar a senha assim, o tipo deve ser NVarChar e grande

            try
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<dynamic>(new { usuario = usuario.Email, senha = senha }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("US01P01 - E-mail já cadastrado."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("US01P01 - Falha interna no servidor."));
            }
        }

        // [AllowAnonymous] -- declara esse parametro quando o Authorize é declarado para a classe inteira
        [HttpPost("v1/usuarios/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] EscolaDataContext context,
            [FromServices] TokenService tokenService)
        {
            //var token = tokenService.GenerateToken(null);
            //return Ok(token);

            if (ModelState.IsValid == false)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = await context
                    .Usuarios
                    .AsNoTracking()
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Email == model.Email)
                    ;

            if (usuario == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário/Senha inválido"));

            //if (PasswordHasher.Verify(usuario.Senha, model.Senha) == false) { } -- comparar o hash do banco com a senha informada
            if (usuario.Senha != model.Senha)
                return StatusCode(402, new ResultViewModel<string>("Usuário/Senha inválido"));

            try
            {
                var token = tokenService.GenerateToken(usuario);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>("US02L01 - Falha interna no servidor."));
            }

        }
    }
}
