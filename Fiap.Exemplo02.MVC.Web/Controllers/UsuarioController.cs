using Fiap.Exemplo02.Dominio.DataAccess;
using Fiap.Exemplo02.MVC.Web.App_Start;
using Fiap.Exemplo02.MVC.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        public UsuarioController()
        {
            this.userManager = IdentityConfig.UserManagerFactory.Invoke();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }
            return returnUrl;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new Usuario
            {
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //cadastrar e loga
                var identity = await userManager.CreateIdentityAsync(
                     user, DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);
                return RedirectToAction("index", "home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new UsuarioViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                  user, DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("", "Usuário e/ou Senha inválidos");
            return View();
        }

        public ActionResult LogOut()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "home");
        }
    }
}