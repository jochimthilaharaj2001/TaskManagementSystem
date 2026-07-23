using taskManagementBE.DTOs;

namespace taskManagementBE.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public int UserId { get; set; }

        
    }
}