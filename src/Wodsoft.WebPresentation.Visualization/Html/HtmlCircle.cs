using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlCircle : HtmlSvgSharp
    {
        public override string Tag { get { return "circle"; } }

        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(HtmlCircle));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(HtmlCircle));
        public double Radius { get { return (double)GetValue(RadiusProperty); } set { SetValue(RadiusProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("cx", Point.X.ToString());
            attributes.Add("cy", Point.Y.ToString());
            attributes.Add("r", Radius.ToString());

            return attributes;
        }
    }
}
