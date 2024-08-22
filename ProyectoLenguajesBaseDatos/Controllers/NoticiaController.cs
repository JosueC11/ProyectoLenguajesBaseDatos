using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajesBaseDatos.Models;
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
            var temas = _noticiaImplement.GetTemas();

            var viewModel = new ModelNoticiasTemas
            {
                Noticias = noticias,
                Temas = temas
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult MisNoticias()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Historial() 
        {
            return View();
        }

        [HttpGet]
        [Route("/Noticia/FiltrarNoticiasTema/{selectedTema}")]
        public ActionResult FiltrarNoticiasTema([FromRoute] string selectedTema)
        {
            var noticiasFiltradasTema = _noticiaImplement.FiltrarNoticiasTema(selectedTema);
            var temas = _noticiaImplement.GetTemas();

            var viewModel = new ModelNoticiasTemas
            {
                Noticias = noticiasFiltradasTema,
                Temas = temas
            };
            return View("Listado", viewModel);
        }

        [HttpGet]
        [Route("/Noticia/FiltrarNoticiasCriterio/{selectedCriterio}")]
        public ActionResult FiltrarNoticiasCriterio(string selectedCriterio)
        {
            var noticiasFiltradasTema = _noticiaImplement.FiltrarNoticiasCriterio(selectedCriterio);
            var temas = _noticiaImplement.GetTemas();

            var viewModel = new ModelNoticiasTemas
            {
                Noticias = noticiasFiltradasTema,
                Temas = temas
            };
            return View("Listado", viewModel);
        }
    }
}
