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
            var tokenDescriptor = new SecurityTokenDescriptor(); // especificação do Token
            var token = tokenHandler.CreateToken(tokenDescriptor); // Cria o Token
            return tokenHandler.WriteToken(token); // retorna uma string baseada no Token
        }


    }
}
