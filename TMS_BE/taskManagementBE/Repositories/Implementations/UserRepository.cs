using taskManagementBE.DTOs;
using taskManagementBE.Models;
using taskManagementBE.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace taskManagementBE.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public User AddUser(CreateUserDto dto)
        {
            User? user = null;

            string query = @"
        INSERT INTO Users(UserName, Email)
        OUTPUT INSERTED.UserId
        VALUES(@UserName, @Email)";

            using (SqlConnection con =
                new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@UserName", dto.UserName);
                cmd.Parameters.AddWithValue("@Email", dto.Email);

                con.Open();

                int userId = (int)cmd.ExecuteScalar();

                user = new User
                {
                    UserId = userId,
                    UserName = dto.UserName,
                    Email = dto.Email
                };
            }

            return user;
        }
    }
}
