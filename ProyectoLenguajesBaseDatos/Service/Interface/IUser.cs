namespace ProyectoLenguajesBaseDatos.Service.Interface
{
    public interface IUser
    {
        int Login(string correo, string password);
        int Registrar(string correo, string nombre, string apellido, string password, int activo);
    }
}
