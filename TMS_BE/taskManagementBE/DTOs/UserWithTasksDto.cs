using System.Collections.Generic;
using taskManagementBE.Models;

namespace taskManagementBE.DTOs
{
    public class UserWithTasksDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
