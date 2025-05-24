
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<IRepository, NamesUnique>();
services.AddTransient<MySystem>();
var serviceProvider = services.BuildServiceProvider();
var mySystem = serviceProvider.GetRequiredService<MySystem>(); 
mySystem.Run();


// Capa Dominio
// Interfces/Aplicacion
public interface IRepository
{
    void Save(string name);
    IEnumerable<string> Get();
}

// Capa Datos
// Componentes Names

public class Names : IRepository
{
    private readonly List<string> _names = new();

    public void Save(string name)
    {
        _names.Add(name);
    }

    public IEnumerable<string> Get() => _names;

}

// Capa Datos
// Componentes Names

public class NamesUnique : IRepository
{
    private readonly HashSet<string> _names = new();

    public void Save(string name)
    {
        _names.Add(name);
    }

    public IEnumerable<string> Get() => _names;

}



// Capa Presentacion
// Componente MySystem

public class MySystem
{
    private IRepository _repository;

    public MySystem(IRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nSeleccionar una opción:");
            Console.WriteLine("1 - Agregar un nombre");
            Console.WriteLine("2 - Mostrar nombres almacenados");
            Console.WriteLine("3 - Salir");
            Console.Write("Opción: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Ingrese un nombre: ");
                    string name = Console.ReadLine();
                    _repository.Save(name);
                    break;
                case "2":
                    Console.WriteLine("\nNombres almacenados:");
                    foreach (var currentName in _repository.Get())
                    {
                        Console.WriteLine($"- {currentName}");
                    }
                    break;
                case "3":
                    Console.WriteLine("Saliendo del sistema....");
                    return;
                default:
                    Console.WriteLine("Opción no válida, intente de nuevo");
                    break;
            }
        }
    }
}