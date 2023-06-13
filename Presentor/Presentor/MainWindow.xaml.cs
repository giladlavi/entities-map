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

            consumer.Handler = (Event entity) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    RenderEntity(entity);
                });
            };
           
            consumer.Start();
        }

        private void RenderEntity(Event entity)
        {
            EllipseEntity el = new EllipseEntity(entity.X, entity.Y, 25, 25);
            var text = new TextBlock();
            text.Text = entity.Name;
            Canvas.SetTop(text, entity.Y + 26);
            Canvas.SetLeft(text, entity.X);

            EntitiesMap.Children.Add(el.Create());
            EntitiesMap.Children.Add(text);
        }
    }
}
