using Newtonsoft.Json;
using Npgsql;
using PedidosService.Data;
using PedidosService.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class RabbitMQConsumer : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMQConsumer> _logger;

    private IConnection _connection;
    private IModel _channel;

    public RabbitMQConsumer(IServiceProvider serviceProvider, ILogger<RabbitMQConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        try
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _logger.LogInformation("Conexão com RabbitMQ estabelecida.");

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "pedidos", durable: true, exclusive: false, autoDelete: false, arguments: null);

            _logger.LogInformation("Fila '{Queue}' declarada.", "pedidos");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao inicializar conexão com RabbitMQ.");
            throw;
        }
    }

    public void IniciarConsumo()
    {
        try
        {
            _logger.LogInformation("Iniciando consumo de mensagens...");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();

                if (body.Length == 0)
                {
                    _logger.LogWarning("Mensagem vazia recebida.");
                    return;
                }

                var message = Encoding.UTF8.GetString(body);

                await ProcessarPedidoAsync(message);
            };

            _channel.BasicConsume(queue: "pedidos", autoAck: true, consumer: consumer);

            _logger.LogInformation("Consumidor aguardando mensagens.");
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Erro ao iniciar consumo.");
        }
    }

    private async Task ProcessarPedidoAsync(string mensagem)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PedidosDbContext>();

            var pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);

            if (pedido == null)
            {
                _logger.LogWarning("Erro: Mensagem não pode ser desserializada.");
                return;
            }

            await dbContext.Pedidos.AddAsync(pedido);

            await dbContext.SaveChangesAsync();
            _logger.LogInformation($"Pedido processado com sucesso: {pedido.CodigoPedido}");
        }
        catch (PostgresException ex)
        {
            _logger.LogError(ex, "Erro específico do PostgreSQL: {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar mensagem.");
        }
    }

    public void PublicarMensagem(string mensagem)
    {
        try
        {
            if (_connection == null || !_connection.IsOpen)
                throw new InvalidOperationException("Conexão com RabbitMQ não está aberta.");
            if (_channel == null || !_channel.IsOpen)
                throw new InvalidOperationException("Canal com RabbitMQ não está aberto.");

            _logger.LogInformation("Iniciando publicação da mensagem...");

            _channel.QueueDeclare(queue: "pedidos", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(mensagem);
            _channel.BasicPublish(exchange: "", routingKey: "pedidos", basicProperties: null, body: body);

            _logger.LogInformation($"Mensagem publicada: {mensagem}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao publicar mensagem.");
        }
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        _logger.LogInformation("Recursos do RabbitMQ liberados.");
    }
}
