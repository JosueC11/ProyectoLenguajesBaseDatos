using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;

namespace ProyectoLenguajesBaseDatos.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly NoticiaImplement _noticiaImplement;

        public NoticiaController(NoticiaImplement noticiaImplement) 
        {
            _noticiaImplement = noticiaImplement;
        }

        [HttpGet]
        public ActionResult Listado()
        {
            var noticias = _noticiaImplement.GetNoticias();
            return View();
        }

        public ActionResult Historial() 
        {
            return View();
        }
    }
}
