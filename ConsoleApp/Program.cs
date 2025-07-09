using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using Dominio.Services;
using JsonRepository;
using Microsoft.Extensions.DependencyInjection;

string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");

var services = new ServiceCollection();
services.AddTransient<IRepository>(provider => new ProductRepository(pathFile));
services.AddTransient<IService, ProductService>();

var serviceProvider = services.BuildServiceProvider();
var productService = serviceProvider.GetService<IService>();

while (true)
{
    try
    {
        Console.WriteLine("\nSeleccione una opción");
        Console.WriteLine("1 - Agregar un producto");
        Console.WriteLine("2 - Mostrar productos almacenados");
        Console.WriteLine("3 - Salir");
        Console.Write("Opción: ");

        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.Write("Ingrese un producto: ");
                string name = Console.ReadLine();
                Console.Write("Ingrese un precio: ");
                decimal price = decimal.Parse(Console.ReadLine());
                productService.Register(name, price);
            break;

            case "2":
                Console.Write("\nProductos almacenados: ");
                foreach(var product in productService.GetAll())
                {
                    Console.WriteLine($"- {product.Name}: $ {product.Price}");
                }
            break;

            case "3":
                Console.Write("Saliendo del sistema...");
                return;
            default:
                Console.WriteLine("Opción no válida, intente de nuevo.");
                break;
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Ocurrio un error: {ex.Message}");
    }
}