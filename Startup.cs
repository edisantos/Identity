using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityTeste4.Store;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IdentityTeste4.Models;
using IdentityTeste4.Contexto;

namespace IdentityTeste4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Data Source=NOTEBOOK\DBLEMOSINFOTEC;Initial Catalog=DbBarbearia;User ID=sa;Password=@Lemos318730";

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<DataContexto>(
                options => options.UseSqlServer(connectionString, sql =>sql.MigrationsAssembly(migrationAssembly)));
            services.AddIdentityCore<UsuariosIdentity>(options => { });
            services.AddScoped<IUserStore<UsuariosIdentity>, 
                UserOnlyStore<UsuariosIdentity, DataContexto>>();

            services.AddIdentity<UsuariosIdentity, IdentityRole>(x =>
            {
                //x.Password.RequiredLength = 6;
                //x.Password.RequireUppercase = false;
                //x.Password.RequireLowercase = false;
                //x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DataContexto>().AddDefaultTokenProviders();

           
            services.AddControllersWithViews();

            services.AddAuthentication("cookies")
                .AddCookie("cookies", options => options.LoginPath = "/Home/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
