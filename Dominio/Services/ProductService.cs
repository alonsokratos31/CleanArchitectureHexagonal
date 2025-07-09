using Dominio.Entities;
using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;

namespace Dominio.Services
{
    public class ProductService : IService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAll()
                => _repository.GetAll();

        public void Register(string name, decimal price)
        {
           var product = new Product(Guid.NewGuid(), name, price);
            _repository.Save(product);
        }
    }
}
