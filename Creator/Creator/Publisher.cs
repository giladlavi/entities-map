using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Creator
{
    public class Publisher
    {
        private IModel channel;
        const string exchangeName = "coordinates";

        public Publisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var conn = factory.CreateConnection();
            this.channel = conn.CreateModel();
            this.channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
        }

        public void Publish(string name, double x, double y)
        {
            var entity = new Event(name, x, y);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));
            this.channel.BasicPublish(exchange: exchangeName,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
