using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Polyline : Shape
    {
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(Point[]), typeof(Polyline));
        public Point[] Points { get { return (Point[])GetValue(PointsProperty); } set { SetValue(PointsProperty, value); } }

    }
}
