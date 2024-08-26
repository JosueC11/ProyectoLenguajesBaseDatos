namespace ProyectoLenguajesBaseDatos.Models
{
    public class Usuario
    {
        public string CorreoUsuario { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PreferenciaCreador { get; set; } = string.Empty;
        public string PreferenciaTema { get; set; } = string.Empty;
        public int Activo { get; set; } = 1;
    }
}
