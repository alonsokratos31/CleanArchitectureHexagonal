namespace DomainComponent.Interfaces
{
    public interface IRepositoryFactory<TRepository, TExtraData>
    {
        public TRepository create(TExtraData extraData);
    }
}
