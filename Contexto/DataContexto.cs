using IdentityTeste4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityTeste4.Contexto
{
    public class DataContexto:IdentityDbContext<UsuariosIdentity>
    {
        public DataContexto(DbContextOptions<DataContexto> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
