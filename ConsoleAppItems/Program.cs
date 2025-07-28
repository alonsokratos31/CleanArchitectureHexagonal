
using ApplicationComponent;
using DomainComponent.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryComponent;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

var service = new ServiceCollection();
service.AddDbContext<ItemsDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Optional: specify max retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(5), // Optional: specify max delay between retries
            errorNumbersToAdd: null // Optional: add specific error codes to retry on
        );
    });
});
service.AddTransient<IRepository, ItemRepository>();
service.AddTransient<IService, ItemService>();

var serviceProvider = service.BuildServiceProvider();
var itemService = serviceProvider.GetRequiredService<IService>();


while (true)
{
    try
    {
        Console.WriteLine("\nSeleccione una opción:");
        Console.WriteLine("1 - Agregar una tarea");
        Console.WriteLine("2 - Mostrar tareas");
        Console.WriteLine("3 - Salir");
        Console.Write("Opción: ");

        string opcion = Console.ReadLine();
        switch(opcion)
        {
            case "1":
                Console.WriteLine("Ingrese una tarea: ");
                string title = Console.ReadLine();
                await itemService.AddAsync(title);
                break;
            case "2":
                Console.WriteLine("\nObteniendo tareas....");
                var items = await itemService.GetAsync();
                Console.WriteLine("\nTareas almacenadas:");
                foreach (var item in items)
                {
                    Console.WriteLine($"[{(item.IsCompleted ? "x" : " ")}] {item.Title}");
                }
                break;

            case "3":
                Console.WriteLine("Saliendo del sistema...");
                return;
            default:
                Console.WriteLine("Opción no válida, intente de nuevo");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurrio un error: {ex.Message}");
    }
}