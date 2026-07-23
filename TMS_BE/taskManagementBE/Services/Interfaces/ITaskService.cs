using taskManagementBE.DTOs;
using taskManagementBE.Models;

namespace taskManagementBE.Services.Interfaces
{
    public interface ITaskService
    {
        ApiResponse<List<TaskItemResponseDto>> GetAllTasks();

        
        ApiResponse<TaskItemResponseDto> GetTaskById(int id);

        ApiResponse<TaskItemResponseDto> AddTask(CreateTaskItemDto dto);

        ApiResponse<TaskItemResponseDto> UpdateTask(int id, UpdateTaskItemDto dto);

        ApiResponse<bool> DeleteTask(int id);
        ApiResponse<TaskItemResponseDto> ChangeStatus(int id, ChangeStatusDto dto);
    }
}