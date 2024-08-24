using ProyectoLenguajesBaseDatos.Models;

namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface IUser
    {
        int Login(string correo, string password);
        int Registrar(string correo, string nombre, string apellido, string password, int activo);
        Usuario GetPerfil(string correo);
        int ActualizarPerfil(string nombre, string apellido, string correo);
        int CambiarPassword(string password, string correo);
    }
}
