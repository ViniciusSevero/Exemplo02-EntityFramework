using Fiap.Exemplo02.Dominio.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Exemplo02.MVC.Web.App_Start
{
    class IdentityConfig
    {
        public static Func<UserManager<Usuario>> UserManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/usuario/login")
            });
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<Usuario>(
                new UserStore<Usuario>(new UsuarioContext()));
                // permite caracteres alfa numéricos no username
                usermanager.UserValidator = new UserValidator<Usuario>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                return usermanager;
            };
        }
    }
}
