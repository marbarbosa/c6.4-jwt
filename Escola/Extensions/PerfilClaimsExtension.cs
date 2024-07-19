using Escola.Models;
using System.Security.Claims;

namespace Escola.Extensions
{
    public static class PerfilClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Usuario usuario)
        {
            var result = new List<Claim>
            {
                new (ClaimTypes.Name, usuario.Nome)
            };
            result.Add(new Claim(ClaimTypes.Role, usuario.Role.Tipo));

            //O balta fez um lista de perfis. Para funcionar assim, deve mudar no Model de Usuario na coluna Role para IList<Role>
            //E trocar esse add
            //result.Add(new Claim(ClaimTypes.Role, usuario.Roles.Tipo));
            //Para:
            //result.AddRange(usuario.Roles.Select(role => new  Claim(ClaimTypes.Role, role.Tipo))); //transforma o tipo Role em tipo Claim
            return result;
        }
    }
}
