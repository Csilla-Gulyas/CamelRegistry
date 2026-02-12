using CamelRegistry.Entities;
using CamelRegistry.Repositories;
using Microsoft.EntityFrameworkCore;

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
    }
}
