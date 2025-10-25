using Task_3.Models;

namespace Task_3.Interfaces

{
    public interface ITaskRepository
    {
        void AddNewTask(TaskItem task);
        List<TaskItem> ShowAllTasks();
        void UpdateTaskCondition(int id, bool IsCompleted);
        void RemoveTaskById(int id);

        TaskItem GetTaskById(int id);
    }
}
