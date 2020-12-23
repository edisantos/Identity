using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityTeste4.Models
{
    public class RegisterViewModels
    {
        [Display(Name ="Nome Completo")]
        public string FullName { get; set; }

        [Display(Name ="Usuário")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Senha")]
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage ="A senha não atende as regras especificadas. Ex: @Senha123")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
