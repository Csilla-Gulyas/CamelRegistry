using CamelRegistry.Entities;
using CamelRegistry.NewFolder;
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

        public async Task<IEnumerable<Camel?>> GetAllCamelsAsync()
        {
            var camels = await _repository.GetAll().ToListAsync();

            return camels;
        }

        public async Task<Camel?> GetByIdAsync(int id)
        {
            var camel = await _repository.GetByIdAsync(id);

            if (camel == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            return camel;
        }

        public async Task<DeleteDto?> DeleteCamelAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (deleted == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            return new DeleteDto
            {
                Message = $"A {deleted.Name} nevű teve sikeresen törölve lett."
            };
        }
    }
}
