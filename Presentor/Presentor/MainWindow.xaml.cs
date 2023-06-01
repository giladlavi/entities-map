using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;



namespace Presentor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();
            // we create a temporary queue
            var result = channel.QueueDeclare(queue: "", exclusive: true);
            channel.QueueBind(exchange: "coordinates", queue: result.QueueName, routingKey: "");

            // we create a consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entity = JsonSerializer.Deserialize<Entity>(message);
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    AddEventToEntitiesMap(entity);
                });
            };
            
            channel.BasicConsume(queue: result.QueueName,
                     autoAck: true,
                     consumer: consumer);

            InitializeComponent();
        }

        private void AddEventToEntitiesMap(Entity? entity)
        {
            if (entity == null)
            {
                return;
            }

            Ellipse el = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color.FromRgb(255, 0, 0);
            el.Fill = brush;
            el.StrokeThickness = 2;
            el.Stroke = Brushes.Black;
            el.Width = 25;
            el.Height = 25;
            var text = new TextBlock();
            text.Text = entity.Name;
            Canvas.SetTop(el, entity.Y);
            Canvas.SetLeft(el, entity.X);
            Canvas.SetTop(text, entity.Y + 26);
            Canvas.SetLeft(text, entity.X);

            EntitiesMap.Children.Add(el);
            EntitiesMap.Children.Add(text);
        }
    }
}
