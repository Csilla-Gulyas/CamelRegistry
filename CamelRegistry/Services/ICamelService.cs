using CamelRegistry.Entities;

namespace CamelRegistry.Services
{
    public interface ICamelService
    {
        //Task<Camel> AddCamelAsync(Camel camel);
        Task<IEnumerable<Camel>> GetAllCamelsAsync();
        //Task<Camel?> GetByIdAsync(int id);
        //Task<Camel> UpdateCamelAsync(Camel camel);
        //Task<Camel?> DeleteCamelAsync(int id);
    }
}
