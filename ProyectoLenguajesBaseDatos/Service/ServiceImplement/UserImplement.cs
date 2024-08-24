using Oracle.ManagedDataAccess.Client;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.Interface;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoLenguajesBaseDatos.Service.ServiceImplement
{
    public class UserImplement : IUser
    {
        private readonly Context _context;
        public UserImplement(Context context) 
        {
            _context = context;
        }

        public int ActualizarPerfil(string nombre, string apellido, string correo)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_ACTUALIZAR_USUARIO", connection))
                    {
                        var correoUsuario = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoUsuario);

                        var nombreParam = new OracleParameter
                        {
                            ParameterName = "NOMBRE_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = nombre
                        };
                        cmd.Parameters.Add(nombreParam);

                        var apellidoParam = new OracleParameter
                        {
                            ParameterName = "APELLIDO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = apellido
                        };
                        cmd.Parameters.Add(apellidoParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int CambiarPassword(string password, string correo)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_CAMBIAR_PASSWORD", connection))
                    {
                        var correoUsuario = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoUsuario);

                        var nombreParam = new OracleParameter
                        {
                            ParameterName = "PASSWORD_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = password
                        };
                        cmd.Parameters.Add(nombreParam);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public Usuario GetPerfil(string correo)
        {
            var usuarioReady = new Usuario();
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_PORTAL.SP_GET_PERFIL", connection))
                    {
                        var correoParam = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoParam);

                        var perfilParam = new OracleParameter
                        {
                            ParameterName = "PERFIL",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(perfilParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            usuarioReady.CorreoUsuario = reader["CORREO_USUARIO"].ToString();
                            usuarioReady.Nombre = reader["NOMBRE"].ToString();
                            usuarioReady.Apellido = reader["APELLIDO"].ToString();
                            usuarioReady.Password = reader["PASSWORD"].ToString();
                            usuarioReady.Activo = Convert.ToInt32(reader["ACTIVO"]);
                        }
                    }
                }
                return usuarioReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int Login(string correo, string password)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_GET_USUARIO", connection))
                    {
                        var correoUsuario = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoUsuario);

                        var passwordUsuario = new OracleParameter
                        {
                            ParameterName = "PASSWORD_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = password
                        };
                        cmd.Parameters.Add(passwordUsuario);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int Registrar(string correo, string nombre, string apellido, string password, int activo)
        {
            var resultado = 0;
            try
            {
                using (OracleConnection connection = _context.GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand("SP_SET_USUARIO", connection))
                    {
                        var correoUsuario = new OracleParameter
                        {
                            ParameterName = "CORREO_USUARIO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = correo
                        };
                        cmd.Parameters.Add(correoUsuario);

                        var nombreUsuario = new OracleParameter
                        {
                            ParameterName = "NOMBRE_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = nombre
                        };
                        cmd.Parameters.Add(nombreUsuario);

                        var apellidoUsuario = new OracleParameter
                        {
                            ParameterName = "APELLIDO_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = apellido
                        };
                        cmd.Parameters.Add(apellidoUsuario);

                        var passwordUsuario = new OracleParameter
                        {
                            ParameterName = "PASSWORD_IN",
                            OracleDbType = OracleDbType.Varchar2,
                            Direction = ParameterDirection.Input,
                            Value = password
                        };
                        cmd.Parameters.Add(passwordUsuario);

                        var activoUsuario = new OracleParameter
                        {
                            ParameterName = "ACTIVO_IN",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Input,
                            Value = activo
                        };
                        cmd.Parameters.Add(activoUsuario);

                        var resultadoParam = new OracleParameter
                        {
                            ParameterName = "RESULTADO",
                            OracleDbType = OracleDbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);

                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                        resultado = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resultadoParam.Value).ToInt32();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
    }
}
