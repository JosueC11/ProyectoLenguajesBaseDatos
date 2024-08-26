namespace ProyectoLenguajesBaseDatos.Models
{
    public class ModelNoticiaComentario
    {
        public Noticia Noticia { get; set; } = new Noticia();
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
