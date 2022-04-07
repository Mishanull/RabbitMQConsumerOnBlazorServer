using System.Text;
using System.Text.Json;
using Entities;
using Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlazorWithRabbitMQ.Consumer;

public class RabbitMQConsumer : BackgroundService
{
    private IServiceProvider _sp;
    private ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQConsumer(IServiceProvider sp)
    {
        _sp = sp;
        _factory = new ConnectionFactory() {HostName = "localhost", UserName = "guest", Password = "guest"};
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "q.sdj3.product",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            _channel.Dispose();
            _connection.Dispose();
            return Task.CompletedTask;
        }

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received +=   (model, ea) =>
        {
            var body = ea.Body.ToArray();
            Product p = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            Console.WriteLine(p);
            var message = Encoding.UTF8.GetString(body);
            using (var scope = _sp.CreateScope())
            {
                var _dao = scope.ServiceProvider.GetRequiredService<IProductDAO>();
                var state = scope.ServiceProvider.GetService<StateContainer>();
                 _dao.AddProduct(p);
                 state!.Property = "products updated";
            }
            Console.WriteLine(" [x] Received {0}", message);
        };
        _channel.BasicConsume(queue: "q.sdj3.product", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }
}