using Microsoft.Data.SqlClient;

namespace Task_3.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
       
    }
}
