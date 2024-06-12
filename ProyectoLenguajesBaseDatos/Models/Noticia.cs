namespace ProyectoLenguajesBaseDatos.Models
{
    public class Noticia
    {
        public int Id { get; set; } = 0;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly PostedAt { get; set; }
    }
}
