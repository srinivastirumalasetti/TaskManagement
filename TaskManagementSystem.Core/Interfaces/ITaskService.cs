using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Interfaces
{
    public interface ITaskService
    {
        List<TaskViewModel> GetAssignedTasks();
        string UpdateTask(TaskViewModel taskViewModel);
        
    }
}
