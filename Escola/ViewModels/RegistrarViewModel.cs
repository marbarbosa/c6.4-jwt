using System.ComponentModel.DataAnnotations;

namespace Escola.ViewModels
{
    public class RegistrarViewModel
    {
        [Required(ErrorMessage ="Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="E-mail obrigatório")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }
    }
}
