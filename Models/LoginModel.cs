using System.ComponentModel.DataAnnotations;

namespace IdentityTeste4.Models
{
    public class LoginModel
    {
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
