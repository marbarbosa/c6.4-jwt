using Escola.Extensions;
using Escola.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Escola.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler =new JwtSecurityTokenHandler(); // instancia do Token
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // pegou a chave
            //var tokenDescriptor = new SecurityTokenDescriptor()
            //{
            //    Subject = new System.Security.Claims.ClaimsIdentity( 
            //        new Claim[] {
            //            new Claim(ClaimTypes.Name, "Marcos"),
            //            new Claim(ClaimTypes.Role, "admin"),
            //            new Claim("MeuTipo","zero")
            //        }
            //        ),
            //    Expires = DateTime.UtcNow.AddHours(Configuration.TempoExpiracaoHoras),
            //    SigningCredentials = new SigningCredentials(
            //        new SymmetricSecurityKey(key),
            //        SecurityAlgorithms.HmacSha256Signature)
            //}; // especificação do Token

            var claims = usuario.GetClaims();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(Configuration.TempoExpiracaoHoras),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            }; // especificação do Token

            var token = tokenHandler.CreateToken(tokenDescriptor); // Cria o Token
            return tokenHandler.WriteToken(token); // retorna uma string baseada no Token
        }


    }
}
