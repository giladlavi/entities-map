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

        public MainWindow()
        {

            var factory = new ConnectionFactory { HostName = "localhost" };
            this.conn = factory.CreateConnection();
            this.channel = this.conn.CreateModel();
            channel.ExchangeDeclare(exchange: "coordinates", type: "fanout");

   
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var entity = new Entity(
                EntityName.Text,
                Double.Parse(EntityX.Text),
                Double.Parse(EntityY.Text)
            );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));

            this.channel.BasicPublish(exchange: "coordinates",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
