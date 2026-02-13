using CamelRegistry.DTOs.Requests;
using CamelRegistry.DTOs.Responses;
using CamelRegistry.Entities;

namespace CamelRegistry.Services
{
    public interface ICamelService
    {
        Task<CamelDto> AddCamelAsync(CreateCamelDto camelDto);
        Task<IEnumerable<CamelDto>> GetAllCamelsAsync();
        Task<CamelDto> GetByIdAsync(int id);
        Task<CamelDto> UpdateCamelAsync(int id, UpdateCamelDto camelDto);
        Task<DeleteDto?> DeleteCamelAsync(int id);
    }
}
