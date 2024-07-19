using System.ComponentModel.DataAnnotations.Schema;

namespace Escola.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        //[ForeignKey("Usuarios")]
        //public int UsuarioId { get; set; }
        //public Usuario? Usuario { get; set; }
    }
}
