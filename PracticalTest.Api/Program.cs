using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticalTest.Api.Data;
using PracticalTest.Api.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Heroku: escutar na porta definida pela variável de ambiente PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add Repository
var oracleConn = Environment.GetEnvironmentVariable("OracleConnection") 
    ?? builder.Configuration.GetConnectionString("OracleConnection");

if (!string.IsNullOrEmpty(oracleConn))
{
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
}
else
{
    builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
}

// Add Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Sempre habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Add global exception handling
app.UseMiddleware<GlobalExceptionHandler>();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Após configurar os serviços, popular dados de exemplo se usando in-memory
if (app.Services.GetService<ITransactionRepository>() is InMemoryTransactionRepository)
{
    InMemoryTransactionRepository.SeedExampleData();
    InMemoryTransactionRepository.ImportFromSalesTxt("Sales.txt");
}

app.Run(); 
