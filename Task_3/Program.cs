using Task_3.Factories;
using Task_3.Interfaces;
using Task_3.Models;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
    

        string connectionString = builder.Configuration.GetConnectionString("Connection");

        IConnectionFactory connectionFactory = new ConnectionFactory(connectionString);

        ITaskRepository taskRepository = new TaskRepository(connectionFactory);


        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Get all tasks");
            Console.WriteLine("3. Update condition of task");
            Console.WriteLine("4. Remove task");
            Console.WriteLine("5. Exit");

            Console.Write("Make a choice: ");
            string input = Console.ReadLine();

            int choice = checkChoice(input);

            switch (choice)
            {
                case 1:
                    AddNewTaskMethod(taskRepository);
                    break;
                case 2:
                    ShowAllTasksMethod(taskRepository);
                    break;
                case 3:
                    UpdateTaskConditionMethod(taskRepository);
                    break;
                case 4:
                    RemoveTaskByIdMethod(taskRepository);
                    break;
                case 5:
                    return;

            }
        }


    }

    static void AddNewTaskMethod(ITaskRepository repository)
    {
        Console.Write("Input a name: ");
        string title = checkInput();

        Console.Write("Input a description : ");
        string description = checkInput();

        var task = new TaskItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        repository.AddNewTask(task);
        Console.WriteLine("The task was added");
    }


    static void ShowAllTasksMethod(ITaskRepository repository)
    {
        var tasks = repository.ShowAllTasks();
        if (tasks.Count == 0)
        {
            Console.WriteLine("List is empty ");
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Name: {task.Title}, Status: {(task.IsCompleted ? "Done" : "Not done")}");

        }
    }


    static void UpdateTaskConditionMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        while (true)
        {
            Console.Write("Is the task  made? (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool isCompleted))
            {
                repository.UpdateTaskCondition(id, isCompleted);
                Console.WriteLine("The condition was updated.");
                return;
            }
            else
            {

                Console.WriteLine("Incorrect input of state.");
                continue;
            }
        }

    }

    static void RemoveTaskByIdMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        repository.RemoveTaskById(id);
        Console.WriteLine("The task was deleted.");
        return;

    }


    static string checkInput()
    {
        string input = Console.ReadLine();
        while (true)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {

                return input;
            }

            Console.WriteLine("Input correct data");
            input = Console.ReadLine();
        }
    }

    static int checkChoice(string input)
    {
        while (true)
        {
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int choice) && (input == "1" || input == "2" || input == "3" || input == "4" || input == "5"))
            {
                return choice;

            }

            Console.WriteLine("An incorrect choice. Try again.");
            input = Console.ReadLine();
        }
    }


    static int checkId(ITaskRepository repository)
    {

        while (true)
        {
            Console.Write("Input ID of task : ");
            if (!int.TryParse(Console.ReadLine(), out int result) || repository.GetTaskById(result) == null)
            {
                Console.WriteLine("Incorrect ID.");
                continue;
            }
            int id = result;
            return id;
        }
    }
}

