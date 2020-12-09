using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabBimestral.MODEL;
using TrabBimestral.MODEL.Repos;
using TrabBimestral.VIEW.Models;

namespace TrabBimestral.VIEW.Views
{
    public class LoginController : Controller
    {
        
        private RepositoryLogin _Repository = new RepositoryLogin();
        private RepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();
        // GET: Login
        LoginView user = new LoginView();
        public ActionResult Index()
        {
            Session["usuario"] = null;
            Session["adm"] = null;
            FormsAuthentication.SignOut();           
            return View("Login2", user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> VerificarLogin(LoginView model)
        {
            string userName = model.Usuario;
            string userPass = model.Senha;
            
            bool Loginok = _Repository.VerificaLogin(userName, userPass);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (Loginok == true)
            {
                Usuario usu = new Usuario();
                usu = _RepositoryUsuario.SelecionarPorEmail(userName);
                FormsAuthentication.SetAuthCookie(model.Usuario, false);
                Session["adm"] = (usu.Usu_Admin) ? "true": null;
                Session["usuario"] = userName;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Login Inválido");
            return View("Login2", user);
            //return View(model);

        }
    }
}