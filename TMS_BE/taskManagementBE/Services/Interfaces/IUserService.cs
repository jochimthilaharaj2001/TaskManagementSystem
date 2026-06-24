using taskManagementBE.DTOs;
using taskManagementBE.Models;

namespace taskManagementBE.Services.Interfaces
{
    public interface IUserService
    {
        ApiResponse<User> AddUser(CreateUserDto dto);
    }
}
