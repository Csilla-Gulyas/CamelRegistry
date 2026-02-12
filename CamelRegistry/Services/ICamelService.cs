using CamelRegistry.Entities;
using CamelRegistry.NewFolder;

namespace CamelRegistry.Services
{
    public interface ICamelService
    {
        //Task<Camel> AddCamelAsync(Camel camel);
        Task<IEnumerable<Camel?>> GetAllCamelsAsync();
        Task<Camel?> GetByIdAsync(int id);
        Task<CamelDto> UpdateCamelAsync(int id, CamelDto updateDto);
        Task<DeleteDto?> DeleteCamelAsync(int id);
    }
}
