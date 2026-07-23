using taskManagementBE.DTOs;
using taskManagementBE.Models;

namespace taskManagementBE.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        List<TaskItemResponseDto> GetAllTasks();

        TaskItemResponseDto GetTaskById(int id);

        TaskItemResponseDto AddTask(CreateTaskItemDto dto);

        TaskItemResponseDto UpdateTask(int id, UpdateTaskItemDto dto);

        TaskItemResponseDto ChangeStatus(int id, ChangeStatusDto dto);

        bool DeleteTask(int id);
    }
}