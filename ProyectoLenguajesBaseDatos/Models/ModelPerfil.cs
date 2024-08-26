namespace ProyectoLenguajesBaseDatos.Models
{
    public class ModelPerfil
    {
        public Usuario Usuario { get; set; } = new Usuario();
        public List<Tema> Temas { get; set; } = new List<Tema>();
        public List<string> Correos { get; set; } = new List<string>();
    }
}
