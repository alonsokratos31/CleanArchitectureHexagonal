namespace DomainComponent.Interfaces
{
    public interface ICommonRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);
    }
}
