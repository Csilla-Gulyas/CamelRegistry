namespace CamelRegistry.Entities
{
    public class Camel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Color { get; set; }

        public int HumpCount { get; set; }

        public DateTime LastFed { get; set; }
    }
}
