using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajesBaseDatos.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Home() 
        {
            HttpContext.Session.SetString("Emal", "jcastillor@gmail.com");
            return View();
        }

    }
}
