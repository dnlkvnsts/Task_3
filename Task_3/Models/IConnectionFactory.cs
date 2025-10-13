using Microsoft.Data.SqlClient;

namespace Task_3.Models
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
       
    }
}
