using Oracle.ManagedDataAccess.Client;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.Interface;
using System.Data;

namespace ProyectoLenguajesBaseDatos.Service.ServiceImplement
{
    public class NoticiaImplement : INoticia
    {
        private readonly Context _context;
        public NoticiaImplement(Context context) 
        {
            _context = context;
        }
        public List<Noticia> GetNoticias()
        {
            try
            {
                using (var connection = _context.GetConnection())
                {
                    connection.Open();
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        
        public void SetNoticias(Noticia noticias)
        {
        }
    }
}
