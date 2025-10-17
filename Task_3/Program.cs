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
            Console.WriteLine("Управление задачами:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Показать все задачи");
            Console.WriteLine("3. Обновить статус задачи");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Выход");

            Console.Write("Выберите действие: ");
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
        Console.Write("Введите название: ");
        string title = checkInput();

        Console.Write("Введите описание : ");
        string description = checkInput();

        var task = new TaskItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        repository.AddNewTask(task);
        Console.WriteLine("Задача добавлена");
    }


    static void ShowAllTasksMethod(ITaskRepository repository)
    {
        var tasks = repository.ShowAllTasks();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Список пуст ");
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Название: {task.Title},Статус: {(task.IsCompleted ? "Сделано" : "Не сделано")}");

        }
    }


    static void UpdateTaskConditionMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        while (true)
        {
            Console.Write("Задача сделана? (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool isCompleted))
            {
                repository.UpdateTaskCondition(id, isCompleted);
                Console.WriteLine("Состояние обновлено.");
                return;
            }
            else
            {

                Console.WriteLine("Некорректный ввод состояния.");
                continue;
            }
        }

    }

    static void RemoveTaskByIdMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        repository.RemoveTaskById(id);
        Console.WriteLine("Задача удалена.");
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

            Console.WriteLine("Введите корректное значение");
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

            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            input = Console.ReadLine();
        }
    }


    static int checkId(ITaskRepository repository)
    {

        while (true)
        {
            Console.Write("Введите ID задачи : ");
            if (!int.TryParse(Console.ReadLine(), out int result) || repository.GetTaskById(result) == null)
            {
                Console.WriteLine("Некорректный ID.");
                continue;
            }
            int id = result;
            return id;
        }
    }
}

