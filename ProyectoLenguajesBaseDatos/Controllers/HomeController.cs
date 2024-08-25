using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class HomeController : Controller
    {
        private readonly NoticiaImplement _noticiaImplement;
        public HomeController(NoticiaImplement noticiaImplement) 
        {
            _noticiaImplement = noticiaImplement;
        }
        [HttpGet]
        public IActionResult Home() 
        {
            var noticias = _noticiaImplement.GetNoticiasRecientes();
            return View(noticias);
        }

        [HttpPost]
        public ActionResult FiltrarPalabra(string filtro)
        {
            var resultado = _noticiaImplement.FiltrarPalabra(filtro);
            return View("Home", resultado);
        }
    }
}
