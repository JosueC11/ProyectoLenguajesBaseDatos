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
                    using (OracleCommand cmd = new OracleCommand("SP_LISTAR_NOTICIAS", connection))
                    {
                        var cantidadNoticas = new OracleParameter
                        {
                            ParameterName = "CANT_NOTICIAS",
                            OracleDbType = OracleDbType.Int32,
                            Value = 15,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(cantidadNoticas);

                        var listaNoticias = new OracleParameter
                        {
                            ParameterName = "LISTA_NOTICIAS",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(listaNoticias);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var noticia = new Noticia
                            {
                                IdNoticia = Convert.ToInt32(reader["ID_NOTICIA"]),
                                IdTema = Convert.ToInt32(reader["ID_TEMA"]),
                                CorreoUsuarioCreador = reader["CORREO_USUARIO_CREADOR"].ToString(),
                                Titulo = reader["TITULO"].ToString(),
                                Sinopsis = reader["SINOPSIS"].ToString(),
                                Descripcion = reader["DESCRIPCION"].ToString(),
                                Visitas = Convert.ToInt32(reader["VISITAS"]),
                                FechaPost = Convert.ToDateTime(reader["FECHA_POST"])
                            };

                            noticias.Add(noticia);
                        }
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
