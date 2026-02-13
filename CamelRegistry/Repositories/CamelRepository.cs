using CamelRegistry.Data;
using CamelRegistry.Entities;

namespace CamelRegistry.Repositories
{
    public class CamelRepository : ICamelRepository
    {
        private readonly CamelDbContext _context;

        public CamelRepository(CamelDbContext context)
        {
            _context = context;
        }

        public async Task<Camel> AddAsync(Camel camel)
        {
            await _context.Camels.AddAsync(camel);
            await _context.SaveChangesAsync();
            return camel;
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

        public async Task<Camel?> UpdateAsync(Camel camel)
        {
            var existing = await _context.Camels.FindAsync(camel.Id);
            if (existing == null)
                return null;

            existing.Name = camel.Name;
            existing.Color = camel.Color;
            existing.HumpCount = camel.HumpCount;
            existing.LastFed = camel.LastFed;

            await _context.SaveChangesAsync();
            return existing;
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
