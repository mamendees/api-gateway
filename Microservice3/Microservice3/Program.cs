using Microservice3;
using Microservice3.CacheService;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "Teste Microservice 3";
    options.Configuration = "localhost:6379";
});
builder.Services.AddTransient<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/microservice3/{id}", async ([FromRoute] int id, [FromServices] ICacheService cacheService) =>
{
    var cliente = new Cliente(id, $"Matheus {id}", 20 + id);
    var cacheObject = await cacheService.GetAsync<Cliente>(cliente.Id.ToString());
    if (cacheObject is null)
        await cacheService.SetAsync(cliente.Id.ToString(), cliente);

    return cliente;
})
.WithOpenApi();

app.Run();