namespace ProyectoLenguajesBaseDatos.Models
{
    public class ModelNoticiasTemas
    {
        public List<Noticia> Noticias { get; set; } = new List<Noticia>();
        public List<Tema> Temas { get; set; } = new List<Tema>();
        public int Resultado { get; set; } = 1;
    }
}
