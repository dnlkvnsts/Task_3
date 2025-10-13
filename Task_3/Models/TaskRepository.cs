using Task_3.Models;
using Dapper;

public class TaskRepository : ITaskRepository
{
    private readonly IConnectionFactory _connectionFactory;

    
    public TaskRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
           
    }

    public void AddNewTask(TaskItem task)
    {
       
        using var connection = _connectionFactory.CreateConnection();
        string query = @"INSERT INTO Tasks(Title, Description, IsCompleted, CreatedAt) 
                         VALUES(@Title, @Description, @IsCompleted, @CreatedAt)";
        connection.Execute(query, task);
    }

    public List<TaskItem> ShowAllTasks()
    {
        using var connection = _connectionFactory.CreateConnection();
        string query = "SELECT * FROM Tasks";
        return connection.Query<TaskItem>(query).ToList();
    }

    public void UpdateTaskCondition(int id, bool isCompleted)
    {
        using var connection = _connectionFactory.CreateConnection();
        string query = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id";
        connection.Execute(query, new { Id = id, IsCompleted = isCompleted });
    }

    public void RemoveTaskById(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        string query = "DELETE FROM Tasks WHERE Id = @Id";
        connection.Execute(query, new { Id = id });
    }

    public TaskItem GetTaskById(int id)
    {
        using var connection = _connectionFactory.CreateConnection();  
        string query = "SELECT * FROM Tasks WHERE Id = @Id";
        return connection.QueryFirstOrDefault<TaskItem>(query, new { Id = id });
     
    }

}
