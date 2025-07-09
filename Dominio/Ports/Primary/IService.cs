using Dominio.Entities;

namespace Dominio.Ports.Primary
{
    public interface IService
    {
        void Register(string name, decimal price);
        IEnumerable<Product> GetAll();
    }
}
