using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Ellipse : Shape
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(Ellipse));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(Ellipse));
        public double RadiusX { get { return (double)GetValue(RadiusXProperty); } set { SetValue(RadiusXProperty, value); } }
        
        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(Ellipse));
        public double RadiusY { get { return (double)GetValue(RadiusYProperty); } set { SetValue(RadiusYProperty, value); } }
    }
}
