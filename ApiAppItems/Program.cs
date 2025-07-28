using ApplicationComponent;
using DomainComponent.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryComponent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ItemsDbContext>(options =>
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

builder.Services.AddTransient<IRepository, ItemRepository>();
builder.Services.AddTransient<IService, ItemService>();

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

app.MapGet("/items", async (IService service) => await service.GetAsync())
    .WithName("GetItems");

app.MapPost("/items", async (string title, IService service) =>
{
    await service.AddAsync(title);
    return Results.Created();

}).WithName("AddItems");


app.Run();



