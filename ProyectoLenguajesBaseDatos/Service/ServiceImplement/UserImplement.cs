using Oracle.ManagedDataAccess.Client;
using ProyectoLenguajesBaseDatos.Models;
using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.Interface;
using System.Data;

namespace ProyectoLenguajesBaseDatos.Service.ServiceImplement
{
    public class UserImplement : IUser
    {
        private readonly Context _context;
        public UserImplement(Context context) 
        {
            _context = context;
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
