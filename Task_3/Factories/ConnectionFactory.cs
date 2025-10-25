using Microsoft.Data.SqlClient;
using Task_3.Interfaces;

namespace Task_3.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
          
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();  
                return connection;
                 
        }       
    }
}
