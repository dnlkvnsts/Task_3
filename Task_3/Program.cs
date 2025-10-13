using Task_3.Models;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"Server=DESKTOP-D9JAIFC\SQLEXPRESS;Database=Tasks;Trusted_Connection=True;TrustServerCertificate=True;";

        IConnectionFactory connectionFactory = new ConnectionFactory(connectionString);

        ITaskRepository taskRepository = new TaskRepository(connectionFactory);


        while (true)
        {
            Console.WriteLine("���������� ��������:");
            Console.WriteLine("1. �������� ������");
            Console.WriteLine("2. �������� ��� ������");
            Console.WriteLine("3. �������� ������ ������");
            Console.WriteLine("4. ������� ������");
            Console.WriteLine("5. �����");

            Console.Write("�������� ��������: ");
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
        Console.Write("������� ��������: ");
        string title = checkInput();

        Console.Write("������� �������� : ");
        string description = checkInput();

        var task = new TaskItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        repository.AddNewTask(task);
        Console.WriteLine("������ ���������");
    }


    static void ShowAllTasksMethod(ITaskRepository repository)
    {
        var tasks = repository.ShowAllTasks();
        if (tasks.Count == 0)
        {
            Console.WriteLine("������ ���� ");
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, ��������: {task.Title},������: {(task.IsCompleted ? "�������" : "�� �������")}");

        }
    }


    static void UpdateTaskConditionMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        while (true)
        {
            Console.Write("������ �������? (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool isCompleted))
            {
                repository.UpdateTaskCondition(id, isCompleted);
                Console.WriteLine("��������� ���������.");
                return;
            }
            else
            {

                Console.WriteLine("������������ ���� ���������.");
                continue;
            }
        }

    }

    static void RemoveTaskByIdMethod(ITaskRepository repository)
    {
        int id = checkId(repository);
        repository.RemoveTaskById(id);
        Console.WriteLine("������ �������.");
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

            Console.WriteLine("������� ���������� ��������");
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

            Console.WriteLine("�������� �����. ���������� �����.");
            input = Console.ReadLine();
        }
    }


    static int checkId(ITaskRepository repository)
    {

        while (true)
        {
            Console.Write("������� ID ������ ��� ��������: ");
            if (!int.TryParse(Console.ReadLine(), out int result) || repository.GetTaskById(result) == null)
            {
                Console.WriteLine("������������ ID.");
                continue;
            }
            int id = result;
            return id;
        }
    }
}

