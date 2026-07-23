namespace taskManagementBE.DTOs
{
    public class UpdateTaskItemDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }
    }
}