using System.ComponentModel.DataAnnotations;

namespace CamelRegistry.DTOs.Responses
{
    public class CamelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public int HumpCount { get; set; }
        public DateTime LastFed { get; set; }
    }
}