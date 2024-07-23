using Microsoft.AspNetCore.Mvc;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }
    }
}
