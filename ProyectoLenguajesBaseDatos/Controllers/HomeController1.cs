using Microsoft.AspNetCore.Mvc;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
