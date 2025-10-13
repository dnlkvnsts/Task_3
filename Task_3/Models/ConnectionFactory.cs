using Microsoft.Data.SqlClient;

namespace Task_3.Models
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
