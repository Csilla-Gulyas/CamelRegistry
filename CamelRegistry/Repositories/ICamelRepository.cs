using CamelRegistry.Entities;
using CamelRegistry.NewFolder;

namespace CamelRegistry.Repositories
{
    public interface ICamelRepository
    {
        //Task<Camel> AddAsync(Camel camel);
        IQueryable<Camel> GetAll();
        Task<Camel?> GetByIdAsync(int id);
        Task<Camel?> UpdateAsync(int id, CamelDto updateDto);
        Task<Camel?> DeleteAsync(int id);
    }
}
