using CamelRegistry.DTOs.Responses;
using CamelRegistry.DTOs.Requests;
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

        public async Task<CamelDto> AddCamelAsync(CreateCamelDto camelDto)
        {
            var camel = new Camel
            {
                Name = camelDto.Name,
                Color = camelDto.Color,
                HumpCount = camelDto.HumpCount,
                LastFed = camelDto.LastFed
            };

            var addedCamel = await _repository.AddAsync(camel);

            return new CamelDto
            {
                Id = addedCamel.Id,
                Name = addedCamel.Name,
                Color = addedCamel.Color,
                HumpCount = addedCamel.HumpCount,
                LastFed = addedCamel.LastFed
            };
        }

        public async Task<IEnumerable<CamelDto>> GetAllCamelsAsync()
        {
            var camels = await _repository.GetAll().ToListAsync();

            return camels.Select(c => new CamelDto
            {
                Id = c.Id,
                Name = c.Name,
                Color = c.Color,
                HumpCount = c.HumpCount,
                LastFed = c.LastFed
            });
        }

        public async Task<CamelDto> GetByIdAsync(int id)
        {
            var camel = await _repository.GetByIdAsync(id);

            if (camel == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            return new CamelDto
            {
                Id = camel.Id,
                Name = camel.Name,
                Color = camel.Color,
                HumpCount = camel.HumpCount,
                LastFed = camel.LastFed
            };
        }

        public async Task<CamelDto> UpdateCamelAsync(int id, UpdateCamelDto camelDto)
        {
            var camel = await _repository.GetByIdAsync(id);
            if (camel == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            if (!string.IsNullOrEmpty(camelDto.Name))
                camel.Name = camelDto.Name;

            if (!string.IsNullOrEmpty(camelDto.Color))
                camel.Color = camelDto.Color;

            if (camelDto.HumpCount.HasValue)
                camel.HumpCount = camelDto.HumpCount.Value;

            if (camelDto.LastFed.HasValue)
                camel.LastFed = camelDto.LastFed.Value;

            var updatedCamel = await _repository.UpdateAsync(camel);

            return new CamelDto
            {
                Id = updatedCamel.Id,
                Name = updatedCamel.Name,
                Color = updatedCamel.Color,
                HumpCount = updatedCamel.HumpCount,
                LastFed = updatedCamel.LastFed
            };
        }

        public async Task<DeleteDto> DeleteCamelAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (deleted == null)
                throw new KeyNotFoundException($"A {id} azonosítójú teve nem található.");

            return new DeleteDto
            {
                Message = $"A {(deleted.Name ?? "ismeretlen nevű")} teve sikeresen törölve lett."
            };
        }
    }
}
