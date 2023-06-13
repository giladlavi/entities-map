using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using Presentor.models;

namespace Presentor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();     
            
            var consumer = new Consumer();

            consumer.Handler = (Event e) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    RenderEntity(e);
                });
            };
           
            consumer.Start();
        }

        private void RenderEntity(Event e)
        {
            EllipseEntity el = new EllipseEntity(e.X, e.Y, 25, 25);
            var text = new TextBlock();
            text.Text = e.Name;
            Canvas.SetTop(text, e.Y + 26);
            Canvas.SetLeft(text, e.X);

            EntitiesMap.Children.Add(el.Create());
            EntitiesMap.Children.Add(text);
        }
    }
}
