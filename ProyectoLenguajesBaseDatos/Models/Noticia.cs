using System.Data;

namespace ProyectoLenguajesBaseDatos.Models
{
    public class Noticia
    {
        public int IdNoticia { get; set; }
        public int IdTema { get; set; }
        public string CorreoUsuarioCreador { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Visitas { get; set; }
        public DateTime FechaPost { get; set; }
    }
}
