using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Presentor
{
    internal class EllipseEntity : EntityBase<Ellipse>
    {
        public override Ellipse Create()
        {
            Ellipse el = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color;
            el.Fill = brush;
            el.StrokeThickness = 2;
            el.Stroke = Brushes.Black;
            el.Width = 25;
            el.Height = 25;
            Canvas.SetTop(el, Y);
            Canvas.SetLeft(el, X);

            return el;
        }

        public EllipseEntity(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = Color.FromRgb(255, 0, 0);
        }
    }
}
