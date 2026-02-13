using System.ComponentModel.DataAnnotations;

namespace CamelRegistry.DTOs.Requests
{
    public class CreateCamelDto
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Color { get; set; }

        [Range(1, 2, ErrorMessage = "A púpok száma 1 vagy 2 lehet.")]
        public int HumpCount { get; set; }

        [Required]
        public DateTime LastFed { get; set; }
    }
}
