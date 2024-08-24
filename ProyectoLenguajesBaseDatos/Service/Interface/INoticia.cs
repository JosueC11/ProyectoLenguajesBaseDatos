using ProyectoLenguajesBaseDatos.Models;

namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface INoticia
    {
        List<Noticia> GetNoticias();
        void SetNoticias(Noticia noticia);
        List<Noticia> FiltrarNoticiasTema(string filtro);
        void AumentarVisitas(int id);
        int ComentarNoticia(int idNoticia, string comentario, string correo);
        int CalificarNoticia(int idNoticia, int calificaion, string correo);
        int CompartirNoticia(int idNoticia, string correoEnvia, string correoDestino);
        List<Tema> GetTemas();
        List<Noticia> FiltrarNoticiasCriterio(string criterio);
        List<Noticia> FiltrarNoticiasCriterioUsuario(string criterio, string correo);
    }
}
