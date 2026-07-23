using Microsoft.Data.SqlClient;
using taskManagementBE.DTOs;
using taskManagementBE.Models;
using taskManagementBE.Repositories.Interfaces;

namespace taskManagementBE.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;
        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public TaskItemResponseDto AddTask(CreateTaskItemDto dto)
        {
            string query = @"
        INSERT INTO Tasks (Title, Description, Status, UserId)
        OUTPUT INSERTED.TaskId
        VALUES (@Title, @Description, @Status, @UserId)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", dto.Title);
                    command.Parameters.AddWithValue("@Description", dto.Description);
                    command.Parameters.AddWithValue("@Status", dto.Status);
                    command.Parameters.AddWithValue("@UserId", dto.UserId);


                    int TaskId = (int)command.ExecuteScalar();

                    return new TaskItemResponseDto
                    {
                        TaskId = TaskId,
                        Title = dto.Title,
                        Description = dto.Description,
                        Status = dto.Status,
                        UserId = dto.UserId
                    };

                }
                

            }
            
        }

        public TaskItemResponseDto? ChangeStatus(int id, ChangeStatusDto dto)
        {
            string query = @"UPDATE Tasks SET Status = @Status WHERE TaskId = @TaskId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", dto.Status);
                    command.Parameters.AddWithValue("@TaskId", id);


                    int rowsAffected = command.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {
                        return new TaskItemResponseDto
                        {
                            TaskId = id,
                            Status = dto.Status
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool DeleteTask(int id)
        {
            string query = @"DELETE FROM Tasks WHERE TaskId = @TaskId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }  


        }

        public List<TaskItemResponseDto> GetAllTasks()
        {
            List<TaskItemResponseDto> tasks = new List<TaskItemResponseDto>();

            string query = @"SELECT t.TaskId, t.Title, t.Description, t.Status, t.UserId, u.UserName
                             FROM Tasks t
                             INNER JOIN Users u ON t.UserId = u.UserId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskItemResponseDto
                            {
                                TaskId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2),
                                Status = reader.GetString(3),
                                UserId = reader.GetInt32(4),
                                UserName = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public TaskItemResponseDto GetTaskById(int id)
        {
            string query = @"SELECT t.TaskId, t.Title, t.Description, t.Status, t.UserId, u.UserName
                             FROM Tasks t
                             INNER JOIN Users u ON t.UserId = u.UserId
                             WHERE t.TaskId = @TaskId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TaskId", id);

                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    return new TaskItemResponseDto
                    {
                        TaskId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        Status = reader.GetString(3),
                        UserId = reader.GetInt32(4),
                        UserName = reader.GetString(5)
                    };
                }
                else
                {
                    return null;
                }



            }

        }

        public TaskItemResponseDto UpdateTask(int id, UpdateTaskItemDto dto)
        {
            string query = @"UPDATE Tasks SET Title = @Title, Description = @Description, Status = @Status WHERE TaskId = @TaskId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", dto.Title);
                    command.Parameters.AddWithValue("@Description", dto.Description);
                    command.Parameters.AddWithValue("@Status", dto.Status);
                    command.Parameters.AddWithValue("@UserId", dto.UserId);
                    command.Parameters.AddWithValue("@TaskId", id);


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return null;
                    }
                   
                }


                string selectQuery = @"SELECT t.TaskId, t.Title, t.Description, t.Status, t.UserId, u.UserName
                             FROM Tasks t
                             INNER JOIN Users u ON t.UserId = u.UserId
                             WHERE t.TaskId = @TaskId";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@TaskId", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TaskItemResponseDto
                            {
                                TaskId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2),
                                Status = reader.GetString(3),
                                UserId = reader.GetInt32(4),
                                UserName = reader.GetString(5)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }


                }


        }
    }
}
