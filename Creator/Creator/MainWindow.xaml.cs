﻿using System;
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
        private Publisher publisher;

        public MainWindow()
        {
            InitializeComponent();
            this.publisher = new Publisher();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x;
            double y;
            if (!Double.TryParse(EntityX.Text, out x) || !Double.TryParse(EntityY.Text, out y) || EntityName.Text == "")
            {
                return;
            }

            this.publisher.Publish(EntityName.Text, x, y);
        }

    }
}
