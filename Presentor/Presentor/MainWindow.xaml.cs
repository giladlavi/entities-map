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
        private IConnection conn;
        private IModel channel;
        public MainWindow()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            this.conn = factory.CreateConnection();
            this.channel = this.conn.CreateModel();
            var result = this.channel.QueueDeclare(queue: "", exclusive: true);
            this.channel.QueueBind(exchange: "coordinates", queue: result.QueueName, routingKey: "");

            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entity = JsonSerializer.Deserialize<Entity>(message);
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    // Create a red Ellipse.
                    Ellipse myEllipse = new Ellipse();

                    // Create a SolidColorBrush with a red color to fill the
                    // Ellipse with.
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                    // Describes the brush's color using RGB values.
                    // Each value has a range of 0-255.
                    mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                    myEllipse.Fill = mySolidColorBrush;
                    myEllipse.StrokeThickness = 2;
                    myEllipse.Stroke = Brushes.Black;
                    // Set the width and height of the Ellipse.
                    myEllipse.Width = 25;
                    myEllipse.Height = 25;
                    var text = new TextBlock();
                    text.Text = entity.Name;
                    Canvas.SetTop(myEllipse, entity.Y);
                    Canvas.SetLeft(myEllipse, entity.X);
                    Canvas.SetTop(text, entity.Y + 26);
                    Canvas.SetLeft(text, entity.X);


                    EntitiesMap.Children.Add(myEllipse);
                    EntitiesMap.Children.Add(text);
                });
            };
            
            this.channel.BasicConsume(queue: result.QueueName,
                     autoAck: true,
                     consumer: consumer);


            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
