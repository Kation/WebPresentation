using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Rectangle : Shape
    {
        public static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(Rectangle));
        public Rect Rect { get { return (Rect)GetValue(RectProperty); } set { SetValue(RectProperty, value); } }
        
        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(Rectangle));
        public double RadiusX { get { return (double)GetValue(RadiusXProperty); } set { SetValue(RadiusXProperty, value); } }

        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(Rectangle));
        public double RadiusY { get { return (double)GetValue(RadiusYProperty); } set { SetValue(RadiusYProperty, value); } }
    }
}
