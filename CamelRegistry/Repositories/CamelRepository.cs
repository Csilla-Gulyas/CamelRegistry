using CamelRegistry.Data;
using CamelRegistry.Entities;
using CamelRegistry.NewFolder;

namespace CamelRegistry.Repositories
{
    public class CamelRepository : ICamelRepository
    {
        private readonly CamelDbContext _context;

        public CamelRepository(CamelDbContext context)
        {
            _context = context;
        }

        public IQueryable<Camel> GetAll()
        {
            return _context.Camels.AsQueryable();
        }

        public async Task<Camel?> GetByIdAsync(int id)
        {
            return await _context.Camels
                .FindAsync(id);
        }

        public async Task<Camel?> UpdateAsync(int id, CamelDto updateDto)
        {
            var camel = await _context.Camels.FindAsync(id);

            if (camel == null)
                return null;

            if (!string.IsNullOrEmpty(updateDto.Name))
                camel.Name = updateDto.Name;

            if (!string.IsNullOrEmpty(updateDto.Color))
                camel.Color = updateDto.Color;

            if (updateDto.HumpCount.HasValue)
                camel.HumpCount = updateDto.HumpCount.Value;

            if (updateDto.LastFed.HasValue)
                camel.LastFed = updateDto.LastFed.Value;

            await _context.SaveChangesAsync();
            return camel;
        }

        public async Task<Camel?> DeleteAsync(int id)
        {
            var camel = await _context.Camels.FindAsync(id);

            if (camel == null)
                return null;

            _context.Camels.Remove(camel);
            await _context.SaveChangesAsync();

            return camel;
        }
    }
}
