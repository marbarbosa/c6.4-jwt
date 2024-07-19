using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Escola.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public string Login { get; set; }
        public string? Senha { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltAtu { get; set; }

        public DateTime? DataExclusao { get; set; }

        [ForeignKey("Roles")]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
