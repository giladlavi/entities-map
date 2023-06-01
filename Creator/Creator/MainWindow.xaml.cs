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
using System.Threading.Channels;

namespace Creator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConnection conn;
        private IModel channel;
        const string exchangeName = "coordinates";

        public MainWindow()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            this.conn = factory.CreateConnection();
            this.channel = this.conn.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x;
            double y;
            if (!Double.TryParse(EntityX.Text, out x) || !Double.TryParse(EntityY.Text, out y) || EntityName.Text == "")
            {
                return;
            }
           
            var entity = new Entity(
                EntityName.Text,
                x,
                y
            );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));

            this.channel.BasicPublish(exchange: exchangeName,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

    }
}
