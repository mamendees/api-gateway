using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ocelot
builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);

//builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
//    .AddJsonFile("appsettings.json", false)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
//    .AddJsonFile("configuration.json")
//    .AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Ocelot
await app.UseOcelot();

app.Run();



