using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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

            var resultado = 1;

            if (TempData["Resultado"] != null)
            {
                resultado = Convert.ToInt32(TempData["Resultado"]);
            }

            var viewModel = new ModelNoticiasTemas
            {
                Noticias = noticias,
                Temas = temas,
                Resultado = resultado
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

        [HttpGet]
        [Route("/Noticia/FiltrarNoticiasCriterioUsuario/{selectedCriterio}")]
        public ActionResult FiltrarNoticiasCriterioUsuario(string selectedCriterio)
        {
            var correo = HttpContext.Session.GetString("email");
            var noticiasFiltradasCriterioUsuario = _noticiaImplement.FiltrarNoticiasCriterioUsuario(selectedCriterio, correo);

            return View("Historial", noticiasFiltradasCriterioUsuario);
        }

        [HttpPost]
        public IActionResult CalificarNoticia(int idNoticia, int calificacion)
        {
            var correo = HttpContext.Session.GetString("email");
            var resultado = _noticiaImplement.CalificarNoticia(idNoticia, calificacion, correo);
            TempData["Resultado"] = resultado;
            return RedirectToAction("Listado","Noticia");
        }

        [HttpPost]
        public IActionResult ComentarNoticia(int idNoticia, string comentario)
        {
            var correo = HttpContext.Session.GetString("email");
            var resultado = _noticiaImplement.ComentarNoticia(idNoticia, comentario, correo);
            TempData["Resultado"] = resultado;
            return RedirectToAction("Listado", "Noticia");
        }

        [HttpPost]
        public IActionResult CompartirNoticia(int idNoticia, string correoDestino)
        {
            var correoEnvia = HttpContext.Session.GetString("email");
            var resultado = _noticiaImplement.CompartirNoticia(idNoticia, correoEnvia , correoDestino);
            TempData["Resultado"] = resultado;
            return RedirectToAction("Listado", "Noticia");
        }
    }
}
