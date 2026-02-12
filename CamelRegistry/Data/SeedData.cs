using CamelRegistry.Entities;

namespace CamelRegistry.Data
{
    public class SeedData
    {
        public static void Initialize(CamelDbContext context)
        {
            context.Camels.RemoveRange(context.Camels);

            context.Camels.AddRange(
                new Camel { Name = "Teve", Color = "barna", HumpCount = 1, LastFed = DateTime.Now.AddHours(-5) },
                new Camel { Name = "Ali", Color = "bézs", HumpCount = 2, LastFed = DateTime.Now.AddHours(-2) },
                new Camel { Name = "Tégla", Color = "sötétbarna", HumpCount = 1, LastFed = DateTime.Now.AddHours(-8) },
                new Camel { Name = "Pupi", Color = "sárgásbarna", HumpCount = 2, LastFed = DateTime.Now.AddHours(-10) },
                new Camel { Name = "Kopi", Color = "fehér", HumpCount = 1, LastFed = DateTime.Now.AddHours(-3) }
            );

            context.SaveChanges();
        }
    }
}
