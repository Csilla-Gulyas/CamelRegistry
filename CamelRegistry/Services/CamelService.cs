using CamelRegistry.Entities;
using CamelRegistry.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CamelRegistry.Services
{
    public class CamelService : ICamelService
    {
        private readonly ICamelRepository _repository;

        public CamelService(ICamelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Camel>> GetAllCamelsAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Camel?> DeleteCamelAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (deleted == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            return deleted;
        }
    }
}
