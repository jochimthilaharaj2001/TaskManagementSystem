using taskManagementBE.DTOs;
using taskManagementBE.Models;

namespace taskManagementBE.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(CreateUserDto dto);
        List<User> GetAllUsers();
        User GetUserById(int id);
        UserWithTasksDto GetUserWithTasks(int id);
    }
}
