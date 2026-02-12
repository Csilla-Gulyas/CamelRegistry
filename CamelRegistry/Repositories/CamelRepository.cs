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

        public IQueryable<Camel> GetAll()
        {
            return _context.Camels.AsQueryable();
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
