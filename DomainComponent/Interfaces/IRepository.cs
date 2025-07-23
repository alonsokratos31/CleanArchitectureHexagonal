using DomainComponent.Entities;

namespace DomainComponent.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();

        Task AddAsync(Item item);
    }
}
