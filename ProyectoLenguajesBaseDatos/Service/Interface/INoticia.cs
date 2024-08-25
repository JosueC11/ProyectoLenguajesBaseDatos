using ProyectoLenguajesBaseDatos.Models;

namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface INoticia
    {
        List<Noticia> GetNoticias();
        List<Noticia> GetNoticiasUsuario(string correo);
        List<Noticia> GetNoticiasRecientes();
        Noticia VerNoticia(int idNoticia);
        int SetNoticias(int idTema, int idSubtema,string correo, string titulo, string sinopsis, string descripcion);
        List<Noticia> FiltrarNoticiasTema(string filtro);
        List<Noticia> FiltrarPalabra(string filtro);
        int AumentarVisitas(int idNoticia);
        int ComentarNoticia(int idNoticia, string comentario, string correo);
        int CalificarNoticia(int idNoticia, int calificaion, string correo);
        int CompartirNoticia(int idNoticia, string correoEnvia, string correoDestino);
        List<Tema> GetTemas();
        List<Noticia> FiltrarNoticiasCriterio(string criterio);
        List<Noticia> FiltrarNoticiasCriterioUsuario(string criterio, string correo);
    }
}
