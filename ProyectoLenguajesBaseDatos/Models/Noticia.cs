using System.Data;

namespace ProyectoLenguajesBaseDatos.Models
{
    public class Noticia
    {
        public int IdNoticia { get; set; }
        public Tema Tema { get; set; } = new Tema();
        public SubTema SubTema { get; set; } = new SubTema();
        public string CorreoUsuarioCreador { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Visitas { get; set; } = 0;
        public DateTime FechaPost { get; set; }
        public int CalificacionPromedio { get; set; } = 0;
    }
}
