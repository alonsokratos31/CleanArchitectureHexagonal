namespace Dominio.Entities
{
    public class Product
    {
        private string _name;
        private decimal _price;
        public Guid Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("El nombre no puede ir vacio");
                }

                _name = value;
            }
        }

        public decimal Price
        {
            get => _price;

            set
            {
                if(value <=0)
                {
                    throw new ArgumentException("El precio debe ser mayor a cero");
                }

                _price = value;
            }
        }

        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
