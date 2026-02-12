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
    }
}
