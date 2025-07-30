using ApplicationComponent;
using ApplicationComponent.DTOs;
using DomainComponent.Entities;
using DomainComponent.Interfaces;
using MapperComponent;
using Microsoft.EntityFrameworkCore;
using RepositoryComponent;
using RepositoryComponent.ExtraData;
using RepositoryComponent.Factories;
using RepositoryComponent.Models;

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
builder.Services.AddTransient<ICommonRepository<Note>, NoteRepository>();
builder.Services.AddTransient<IService, ItemService>();
builder.Services.AddTransient<ICommonService<Note>, NoteService>();

// DTO en aplicacion
builder.Services.AddTransient<ICommonRepository<NoteDTO>, NoteDTORepository>();
builder.Services.AddTransient<ICommonService<NoteDTO>, NoteDTOService>();

// MAPPER en la aplicacion
builder.Services.AddTransient<IAddRepository<NoteModel>, NoteMapperRepository>();
builder.Services.AddTransient<IMapper<NoteDTO, Note>, NoteEntityMapper>();
builder.Services.AddTransient<IMapper<NoteDTO, NoteModel>, NoteModelMapper>();
builder.Services.AddTransient<IAddService<NoteDTO, NoteModel>, NoteMapperService<NoteDTO, NoteModel>>();

// Fabrica
builder.Services.AddTransient<IRepositoryFactory<IAddRepository<Note>, NoteExtraData>, NoteRepositoryFactory>();
builder.Services.AddTransient<IMapper<NoteDTO, NoteExtraData>, NoteExtraDataMapper>();
builder.Services.AddTransient<IAddService<NoteDTO, NoteExtraData>, NoteWithFactoryService<NoteDTO, NoteExtraData>>();

// Completar
builder.Services.AddTransient<ICompleteService, CompleteItemService>();
builder.Services.AddTransient<ICompleteRepository, ItemRepository>();
builder.Services.AddTransient<IGetRepository<Item>, ItemRepository>();

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

app.MapGet("/notes", async (ICommonService<Note> service) =>
{
    return await service.GetAsync();
}).WithName("GetNotes");

app.MapPost("/notes", async (Note note, ICommonService<Note> service) =>
{
    await service.AddAsync(note);
    return Results.Created();
}).WithName("AddNotes");

app.MapGet("/notesdto", async (ICommonService<NoteDTO> service) =>
{
    return await service.GetAsync();
}).WithName("GetNotesDTO");

app.MapPost("/notesdto", async (NoteDTO note, ICommonService<NoteDTO> service) =>
{
    await service.AddAsync(note);
    return Results.Created();
}).WithName("AddNotesDto");

app.MapPost("/notesmapper", async (NoteDTO note, IAddService<NoteDTO, NoteModel> service) =>
{
    await service.AddAsync(note);
    return Results.Created();
}).WithName("AddNotesMapper");

app.MapPost("/notesfactory", async (NoteDTO note, IAddService<NoteDTO, NoteExtraData> service) =>
{
    await service.AddAsync(note);
    return Results.Created();
}).WithName("AddNotesFactory");

app.Run();



