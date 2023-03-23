using System.ComponentModel.DataAnnotations;

namespace Todo_List.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public string? Priority { get; set; }
        public string? Note { get; set; }
    }
}
