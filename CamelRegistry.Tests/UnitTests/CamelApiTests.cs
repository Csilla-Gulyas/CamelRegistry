using CamelRegistry.Data;
using CamelRegistry.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;


namespace CamelRegistry.Tests.UnitTests
{
    public class CamelApiTests
    {
        private CamelDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<CamelDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CamelDbContext(options);
        }

        [Fact]
        public void Can_Create_Camel()
        {
            using var context = GetInMemoryDbContext();

            var camel = new Camel { Name = "Alex", Color = "Brown", HumpCount = 2, LastFed = DateTime.Now };
            context.Camels.Add(camel);
            context.SaveChanges();

            context.Camels.Count().Should().Be(1);
            context.Camels.First().Name.Should().Be("Alex");
        }

        [Fact]
        public void Can_Get_Camels()
        {
            using var context = GetInMemoryDbContext();

            context.Camels.Add(new Camel { Name = "Alex", Color = "Brown", HumpCount = 2 });
            context.Camels.Add(new Camel { Name = "Bob", Color = "White", HumpCount = 1 });
            context.SaveChanges();

            var camels = context.Camels.ToList();

            camels.Count.Should().Be(2);
            camels.Any(c => c.Name == "Alex").Should().BeTrue();
            camels.Any(c => c.Name == "Bob").Should().BeTrue();
        }

        [Fact]
        public void Can_Get_Camel_By_Id()
        {
            using var context = GetInMemoryDbContext();

            var camel = new Camel { Name = "Alex", Color = "Brown", HumpCount = 2 };
            context.Camels.Add(camel);
            context.SaveChanges();

            var fetched = context.Camels.Find(camel.Id);
            fetched.Should().NotBeNull();
            fetched.Name.Should().Be("Alex");
        }

        [Fact]
        public void Can_Update_Camel()
        {
            using var context = GetInMemoryDbContext();

            var camel = new Camel { Name = "Alex", Color = "Brown", HumpCount = 2 };
            context.Camels.Add(camel);
            context.SaveChanges();

            camel.Color = "Black";
            context.Camels.Update(camel);
            context.SaveChanges();

            var updated = context.Camels.First();
            updated.Color.Should().Be("Black");
        }

        [Fact]
        public void Can_Delete_Camel()
        {
            using var context = GetInMemoryDbContext();

            var camel = new Camel { Name = "Alex", Color = "Brown", HumpCount = 2 };
            context.Camels.Add(camel);
            context.SaveChanges();

            context.Camels.Remove(camel);
            context.SaveChanges();

            context.Camels.Count().Should().Be(0);
        }
    }
}

