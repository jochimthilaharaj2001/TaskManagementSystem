using taskManagementBE.DTOs;
using taskManagementBE.Models;
using taskManagementBE.Repositories.Interfaces;
using taskManagementBE.Services.Interfaces;

namespace taskManagementBE.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public ApiResponse<TaskItemResponseDto> AddTask(CreateTaskItemDto dto)
        {
            var taskItem = _taskRepository.AddTask(dto);
            return new ApiResponse<TaskItemResponseDto>
            {
                Success = true,
                Message = "Task added successfully",
                Data = taskItem
            };

        }

        public ApiResponse<TaskItemResponseDto> ChangeStatus(int id, ChangeStatusDto dto)
        {
            var taskItem = _taskRepository.GetTaskById(id);

            if (taskItem == null)
            {
                return new ApiResponse<TaskItemResponseDto>
                {
                    Success = false,
                    Message = "Task not found",
                    Data = null
                };
            }



            var updatedTask = _taskRepository.ChangeStatus(id, dto);

            return new ApiResponse<TaskItemResponseDto>
            {
                Success = true,
                Message = "Task status updated successfully",
                Data= updatedTask

            };
        }

        public ApiResponse<bool> DeleteTask(int id)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null) 
            {
                return new ApiResponse<bool>
                {
                    Success= false,
                    Message = "Task not found",
                    Data=false
                };
            }

            bool isDeleted = _taskRepository.DeleteTask(id);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Task deleted successfully" ,
                Data = true
            };

        }

        public ApiResponse<List<TaskItemResponseDto>> GetAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();

            return new ApiResponse<List<TaskItemResponseDto>>
            {
                Success = true,
                Message = "Tasks retrieved successfully",
                Data = tasks
            };
        }

        public ApiResponse<TaskItemResponseDto> GetTaskById(int id)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return new ApiResponse<TaskItemResponseDto>
                {
                    Success = false,
                    Message = "Task not found",
                    Data = null
                };
            }

            return new ApiResponse<TaskItemResponseDto>
            {
                Success = true,
                Message = "Task retrieved successfully",
                Data = task
            };

        }

        public ApiResponse<TaskItemResponseDto> UpdateTask(int id, UpdateTaskItemDto dto)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null) 
            {
                return new ApiResponse<TaskItemResponseDto>
                {
                    Success = false,
                    Message = "Task not found",
                    Data = null
                };
            }

            return new ApiResponse<TaskItemResponseDto>
            {
                Success = true,
                Message = "Task updated successfully",
                Data = _taskRepository.UpdateTask(id, dto)
            };
        }

        
    }
}