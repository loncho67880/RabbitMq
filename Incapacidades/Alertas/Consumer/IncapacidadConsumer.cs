using Core.Incapacidades;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Servicios.AnulacionServicio;
using System.Text;

namespace Alertas.Consumer
{
    public class IncapacidadConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IConfiguration _configuration;
        private IAnulacionServicio _anulacionServicio;
        private const string ExchangeIncapacidadName = "Incapacidad_Exchange";
        private const string AlertaQueueName = "AlertaQueueName";
        private const string AlertaQueueNameRouteKey = "AlertaQueueNameRouteKey";

        public IncapacidadConsumer(IConfiguration configuration, IAnulacionServicio anulacionServicio)
        {
            _configuration = configuration;
            _anulacionServicio = anulacionServicio;
            var factory = new ConnectionFactory
            {
                HostName = _configuration["Rabbit:HostName"],
                UserName = _configuration["Rabbit:UserName"],
                Password = _configuration["Rabbit:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeIncapacidadName, ExchangeType.Direct);
            _channel.QueueDeclare(AlertaQueueName, false, false, false, null);
            _channel.QueueBind(AlertaQueueName, ExchangeIncapacidadName, AlertaQueueNameRouteKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Incapacidad incapacidad = JsonConvert.DeserializeObject<Incapacidad>(content);
                _anulacionServicio.Anularincapacidad(incapacidad);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(AlertaQueueName, false, consumer);

            return Task.CompletedTask;
        }
    }
}
