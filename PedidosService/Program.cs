using Microsoft.EntityFrameworkCore;
using Npgsql;
using PedidosService.Data;
using PedidosService.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

// Conexão com banco de dados
builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Loggers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<RabbitMQConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pedidos v1");
    });
}

try
{
    var consumer = app.Services.GetRequiredService<RabbitMQConsumer>();
    consumer.IniciarConsumo();
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao iniciar o consumidor do RabbitMQ: {ex.Message}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

//string connectionString = "Host=172.17.0.2;Port=5432;Database=BancoPedidos;Username=postgres;Password=123";

//try
//{
//    using var conn = new NpgsqlConnection(connectionString);
//    conn.Open();
//    Console.WriteLine("Conexão com o banco foi estabelecida com sucesso.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Erro ao conectar ao banco: {ex.Message}");
//}

app.Run();
