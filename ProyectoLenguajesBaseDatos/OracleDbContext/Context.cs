using Oracle.ManagedDataAccess.Client;

namespace ProyectoLenguajesBaseDatos.OracleDbContext
{
    public class Context
    {
        private readonly OracleConnection _connection;

        public Context(string connectionString) 
        { 
            _connection = new OracleConnection(connectionString);
        }

        public OracleConnection GetConnection() 
        { 
            return _connection;
        }
    }
}
