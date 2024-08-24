using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class UserController : Controller
    {
        private readonly UserImplement _userImplement;
        public UserController(UserImplement userImplement) 
        { 
            _userImplement = userImplement;
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
            var resultado = _userImplement.GetPerfil(correo);
            return View("Perfil", resultado);
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
    }
}
