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

        public DTOs.ApiResponse<User> AddUser(CreateUserDto dto)
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
    }

}
