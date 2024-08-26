using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class UserController : Controller
    {
        private readonly UserImplement _userImplement;
        private readonly NoticiaImplement _noticiasImplement;
        public UserController(UserImplement userImplement, NoticiaImplement noticiaImplement) 
        { 
            _userImplement = userImplement;
            _noticiasImplement = noticiaImplement;
        }

        [HttpGet]
        public IActionResult GetLogin()
        {
            return View("Login",1);
        }

        [HttpPost]
        public IActionResult SetLogin(string email, string password)
        {
            var resultado = _userImplement.Login(email,password);
            if(resultado == 1) 
            {
                HttpContext.Session.SetString("email", email);
                return RedirectToAction("Home","Home");
            }
            return View("Login",resultado);
        }

        [HttpGet]
        public IActionResult GetSingUp()
        {
            return View("SingUp", 1);
        }

        [HttpPost]
        public IActionResult SetSingUp(string email, string nombre, string apellido, string password)
        {
            var resultado = _userImplement.Registrar(email,nombre,apellido,password, 1);
            if (resultado == 1)
            {
                return RedirectToAction("GetLogin", "User");
            }
            return View("SingUp", resultado);
        }

        [HttpGet]
        public IActionResult GetPerfil()
        {
            var correo = HttpContext.Session.GetString("email");
            if (correo is null)
            {
                return RedirectToAction("GetLogin", "User");
            }
            var resultado = _userImplement.GetPerfil(correo);
            var temas = _noticiasImplement.GetTemas();
            var correos = _userImplement.GetCorreosUsuarios();
            var viewModel = new ModelPerfil
            {
                Correos = correos,
                Usuario = resultado,
                Temas = temas,
            };
            return View("Perfil", viewModel);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(string nombre, string apellido)
        {
            var correo = HttpContext.Session.GetString("email");
            var resultado = _userImplement.ActualizarPerfil(nombre, apellido, correo);
            if (resultado == 1) 
            {
                return RedirectToAction("GetPerfil","User");
            }
            return View("Perfil");
        }

        [HttpPost]
        public IActionResult CambiarPassword(string password)
        {
            var correo = HttpContext.Session.GetString("email");
            var resultado = _userImplement.CambiarPassword(password, correo);
            if (resultado == 1)
            {
                return RedirectToAction("GetPerfil", "User");
            }
            return View("Perfil");
        }

        [HttpPost]
        public IActionResult ActualizarPreferencias(string preferenciaCreador, string preferenciaTema)
        {
            var correo = HttpContext.Session.GetString("email");
            var resultado = _userImplement.ActualizarPreferencias(preferenciaCreador, preferenciaTema, correo);
            return RedirectToAction("GetPerfil", "User");
        }
    }
}
