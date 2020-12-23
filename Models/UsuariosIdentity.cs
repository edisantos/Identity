using Microsoft.AspNetCore.Identity;

namespace IdentityTeste4.Models
{
    public class UsuariosIdentity:IdentityUser
    {
        public  string FullName { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
