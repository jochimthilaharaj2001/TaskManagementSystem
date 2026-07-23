using taskManagementBE.DTOs;
using taskManagementBE.Models;
using taskManagementBE.Repositories.Interfaces;
using taskManagementBE.Services.Interfaces;

namespace taskManagementBE.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Add User
        public ApiResponse<User> AddUser(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName))
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "UserName is required"
                };
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "Email is required"
                };
            }

            var user = _userRepository.AddUser(dto);

            return new ApiResponse<User>
            {
                Success = true,
                Message = "User added successfully",
                Data = user
            };
        }

        // Get All Users
        public ApiResponse<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            return new ApiResponse<IEnumerable<User>>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = users
            };
        }

        // Get User By Id
        public ApiResponse<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            return new ApiResponse<User>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = user
            };
        }

        // Get User With Tasks
        public ApiResponse<UserWithTasksDto> GetUserWithTasks(int id)
        {
            var user = _userRepository.GetUserWithTasks(id);

            if (user == null)
            {
                return new ApiResponse<UserWithTasksDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            return new ApiResponse<UserWithTasksDto>
            {
                Success = true,
                Message = "User with tasks retrieved successfully",
                Data = user
            };
        }

        
    }
}