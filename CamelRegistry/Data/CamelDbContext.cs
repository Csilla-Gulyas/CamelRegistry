using CamelRegistry.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Xml.Linq;

namespace CamelRegistry.Data
{
    public class CamelDbContext:DbContext
    {
        public CamelDbContext(DbContextOptions<CamelDbContext> options)
            : base(options) { }

        public DbSet<Camel> Camels { get; set; }
    }
}
