using ProyectoLenguajesBaseDatos.Models;

namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface INoticia
    {
        List<Noticia> GetNoticias();

        void SetNoticias(Noticia noticia);

        List<Noticia> FiltrarNoticiasTema(string filtro);

        void AumentarVisitas(int id);

        void ComentarNoticia(string comentario, int id);

        void CalificarNoticia(int calificaion, int id);

        void CompartirNoticia(string correoDestino, int id);

        List<Tema> GetTemas();

        List<Noticia> FiltrarNoticiasCriterio(string criterio);
    }
}
