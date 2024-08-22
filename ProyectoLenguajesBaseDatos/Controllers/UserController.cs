using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;

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
    }
}
