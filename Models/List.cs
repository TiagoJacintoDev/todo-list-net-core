using System.Text.Json.Serialization;

namespace Todo_List.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Task> Tasks { get; set; }
    }
}
