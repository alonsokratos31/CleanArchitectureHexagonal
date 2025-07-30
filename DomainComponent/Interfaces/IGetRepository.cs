namespace DomainComponent.Interfaces
{
    public interface IGetRepository<TEntity>
    {
        public Task<TEntity?> GetByIdAsync(int id);
    }
}
