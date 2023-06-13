using Presentor.models;
using Presentor;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Presentor
{
    public class Consumer
    {
        private IModel channel;
        private string queueName;
        private EventingBasicConsumer consumer;
        public Action<Event>? Handler { get; set; }

        public Consumer()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var conn = factory.CreateConnection();
            this.channel = conn.CreateModel();

            // we create a temporary queue
            var result = channel.QueueDeclare(queue: "", exclusive: true);
            this.queueName = result.QueueName;
            channel.QueueBind(exchange: "coordinates", queue: this.queueName, routingKey: "");

            // we create a consumer
            this.consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var e = JsonSerializer.Deserialize<Event>(message);
                            if (e != null && Handler != null) {
                                Handler(e);
                            }
                        };
        }

        public void Start()
        {
            this.channel.BasicConsume(queue: this.queueName,
             autoAck: true,
             consumer: this.consumer);
        }
    }
}
