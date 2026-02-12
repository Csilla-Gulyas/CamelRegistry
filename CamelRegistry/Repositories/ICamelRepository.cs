using CamelRegistry.Entities;

namespace CamelRegistry.Repositories
{
    public interface ICamelRepository
    {
        //Task<Camel> AddAsync(Camel camel);
        IQueryable<Camel> GetAll();
        //Task<Camel?> GetByIdAsync(int id);
        //Task<Camel> UpdateAsync(Camel camel);
        Task<Camel?> DeleteAsync(int id);
    }
}
