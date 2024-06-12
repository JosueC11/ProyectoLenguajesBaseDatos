using ProyectoLenguajesBaseDatos.Models;

namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface INoticia
    {
        List<Noticia> GetNoticias();

        void SetNoticias(Noticia noticia);
    }
}
