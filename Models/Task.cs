using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Todo_List.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Note { get; set; }

        [JsonIgnore]
        public List List { get; set; }
    }
}
