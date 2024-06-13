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
            var noticias = new List<Noticia>();

            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("ObtenerRegistros", connection))
                    {
                        var registros = new OracleParameter();
                        registros.ParameterName = "registros";
                        registros.OracleDbType = OracleDbType.RefCursor;
                        registros.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(registros);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia();

                            noticia.Id = Convert.ToInt32(reader["ID_NOTICIA"]);
                            noticia.Title = reader["TITLE"].ToString();
                            noticia.Description = reader["DESCRIPTION"].ToString();
                            noticia.PostedAt = Convert.ToDateTime(reader["POSTEDAT"]);

                            noticias.Add(noticia);
                        }

                        connection.Close();
                    }
                }
                return noticias;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        
        public void SetNoticias(Noticia noticias)
        {
        }
    }
}
