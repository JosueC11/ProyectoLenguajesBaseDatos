using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ProyectoLenguajesBaseDatos.OracleDbContext
{
    public class Context
    {
        private readonly OracleConnection _connection;

        public Context(string connectionString) 
        { 
            _connection = new OracleConnection(connectionString);
        }

        public void Open() 
        { 
            if(_connection.State != ConnectionState.Open) 
            { 
                _connection.Open();
            }
        }

        public void Close()
        {
            if(_connection.State != ConnectionState.Closed) 
            { 
                _connection.Close();
            }
        }
    }
}
