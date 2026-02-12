namespace CamelRegistry.NewFolder
{
    public class CamelDto
    {
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public int HumpCount { get; set; }
        public DateTime LastFed { get; set; }
    }

    public class DeleteDto
    {
        public string Message { get; set; } = null!;
    }
}