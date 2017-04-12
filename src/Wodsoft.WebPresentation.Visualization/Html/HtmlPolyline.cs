using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlPolyline : HtmlSvgSharp
    {
        public override string Tag { get { return "polyline"; } }

        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(Point[]), typeof(HtmlPolyline));
        public Point[] Points { get { return (Point[])GetValue(PointsProperty); } set { SetValue(PointsProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            if (HasValue(PointsProperty) && Points != null)
                attributes.Add("points", string.Join(" ", Points.Select(t => t.ToString(","))));

            return attributes;
        }
    }
}
