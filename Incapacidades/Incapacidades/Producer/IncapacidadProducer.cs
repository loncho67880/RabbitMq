using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Incapacidades.Producer
{
    public class IncapacidadProducer : IIncapacidadProducer
    {
        private IConfiguration _configuration;

        private const string ExchangeIncapacidadName = "Incapacidad_Exchange";
        private const string EmailQueueName = "EmailQueueName";
        private const string AlertaQueueName = "AlertaQueueName";
        private const string EmailQueueNameRouteKey = "EmailQueueNameRouteKey";
        private const string AlertaQueueNameRouteKey = "AlertaQueueNameRouteKey";

        public IncapacidadProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Enviar mensaje
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public void SendNotificatinoIncapacidadMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = _configuration["Rabbit:HostName"],
                UserName = _configuration["Rabbit:UserName"],
                Password = _configuration["Rabbit:Password"]
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.ExchangeDeclare(ExchangeIncapacidadName, ExchangeType.Direct, durable: false);
            channel.QueueDeclare(EmailQueueName, false, false, false, null);
            channel.QueueDeclare(AlertaQueueName, false, false, false, null);

            channel.QueueBind(EmailQueueName, ExchangeIncapacidadName, EmailQueueNameRouteKey);
            channel.QueueBind(AlertaQueueName, ExchangeIncapacidadName, AlertaQueueNameRouteKey);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: ExchangeIncapacidadName, routingKey: EmailQueueNameRouteKey, body: body);
            channel.BasicPublish(exchange: ExchangeIncapacidadName, routingKey: AlertaQueueNameRouteKey, body: body);
        }
    }
}
