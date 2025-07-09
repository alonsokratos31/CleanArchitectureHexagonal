using Dominio.Entities;

namespace Dominio.Ports.Secundary
{
    public interface IRepository
    {
        void Save(Product product);
        List<Product> GetAll();
    }
}
