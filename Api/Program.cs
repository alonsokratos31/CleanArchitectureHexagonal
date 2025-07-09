using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using Dominio.Services;
using XMLRepository;
//using JsonRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
string pathFile = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "products.xml");

builder.Services.AddTransient<IRepository>(provider => new XMLProductRepository(pathFile));
builder.Services.AddTransient<IService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/products", (IService service) =>
{
    return service.GetAll();
}).WithName("GetProducts");

app.MapPost("/products", (string name, decimal price, IService service) =>
{
    service.Register(name, price);
    return Results.Created();
}).WithName("AddProducts");


/*var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");*/

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
