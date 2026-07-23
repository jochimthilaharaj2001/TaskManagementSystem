using taskManagementBE.DTOs;
using taskManagementBE.Models;

namespace taskManagementBE.Services.Interfaces
{
    public interface IUserService
    {
        ApiResponse<User> AddUser(CreateUserDto dto);

        ApiResponse<IEnumerable<User>> GetAllUsers();

        ApiResponse<User> GetUserById(int id);

        ApiResponse<UserWithTasksDto> GetUserWithTasks(int id);
    }
}