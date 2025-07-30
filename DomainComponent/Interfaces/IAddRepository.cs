namespace DomainComponent.Interfaces
{
    public interface IAddRepository<TModel>
    {
        Task AddAsync(TModel model);
    }
}
