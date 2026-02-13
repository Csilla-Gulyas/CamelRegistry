using System.ComponentModel.DataAnnotations;

namespace CamelRegistry.DTOs.Requests
{
    public class UpdateCamelDto
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int? HumpCount { get; set; }
        public DateTime? LastFed { get; set; }
    }
}
