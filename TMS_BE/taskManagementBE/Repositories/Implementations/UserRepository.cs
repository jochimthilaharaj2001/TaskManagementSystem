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

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string query = "SELECT UserId, UserName, Email FROM Users";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            string query = @"
        SELECT UserId, UserName, Email
        FROM Users
        WHERE UserId = @UserId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                }
            }

            return null;
        }

        public UserWithTasksDto GetUserWithTasks(int id)
{
    UserWithTasksDto user = null;

    string query = @"
    SELECT 
        u.UserId,
        u.UserName,
        u.Email,
        t.TaskId,
        t.Title,
        t.Description,
        t.Status
    FROM Users u
    LEFT JOIN Tasks t ON u.UserId = t.UserId
    WHERE u.UserId = @UserId";

    using (SqlConnection con = new SqlConnection(_connectionString))
    {
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@UserId", id);

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            if (user == null)
            {
                user = new UserWithTasksDto
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    UserName = reader["UserName"].ToString(),
                    Email = reader["Email"].ToString()
                };
            }

                    if (reader["TaskId"] != DBNull.Value)
                    {
                        user.Tasks.Add(new TaskItem
                        {
                            TaskId = Convert.ToInt32(reader["TaskId"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
    }

    return user;
}
    }
}
