using IdentityTeste4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityTeste4.Controllers
{
    public class AccountController : Controller
    {
        protected readonly UserManager<UsuariosIdentity> _userManager;
        public AccountController(UserManager<UsuariosIdentity> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels mod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(mod.UserName);
                    var email = await _userManager.FindByEmailAsync(mod.Email);
                    if (user == null && email == null)
                    {
                        user = new UsuariosIdentity
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = mod.UserName,
                            Email = mod.Email
                        };
                        var result = await _userManager.CreateAsync(
                        user, mod.Password);
                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ViewBag.MsgError = "Usuário ou Email já existe";
                    }
                   
                }
                else
                {
                    ViewBag.MsgError = "Erro ao tentar criar o usuario";
                }
                
            }
            catch (Exception ex)
            {

                ViewBag.MsgError = ex.Message;
            }
            return View(mod);
        }
        public async Task<IActionResult> Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel mod)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(mod.UserName);
               
                if(user!=null && await _userManager.CheckPasswordAsync(user,mod.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("Logado");
                }
                ModelState.AddModelError("", "Usuario ou senha invalida");
            }
            return View(mod);
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Logado()
        {
            return View();
        }
    }
}
