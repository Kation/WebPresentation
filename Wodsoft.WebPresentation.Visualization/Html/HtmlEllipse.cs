using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlEllipse : HtmlSvgSharp
    {
        public override string Tag { get { return "ellipse"; } }

        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(HtmlEllipse));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(HtmlEllipse));
        public double RadiusX { get { return (double)GetValue(RadiusXProperty); } set { SetValue(RadiusXProperty, value); } }

        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(HtmlEllipse));
        public double RadiusY { get { return (double)GetValue(RadiusYProperty); } set { SetValue(RadiusYProperty, value); } }
        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("cx", Point.X.ToString());
            attributes.Add("cy", Point.Y.ToString());
            attributes.Add("rx", RadiusX.ToString());
            attributes.Add("ry", RadiusY.ToString());

            return attributes;
        }
    }
}
