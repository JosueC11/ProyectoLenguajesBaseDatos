using Oracle.ManagedDataAccess.Client;

namespace ProyectoLenguajesBaseDatos.OracleDbContext
{
    public class Context
    {
        private readonly string _connectionString;

        public Context(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        public OracleConnection GetConnection() 
        { 
            return new OracleConnection(_connectionString);
        }
    }
}
