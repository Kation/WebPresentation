using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlRect : HtmlSvgSharp
    {
        public override string Tag { get { return "rect"; } }

        public static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(HtmlRect));
        public Rect Rect { get { return (Rect)GetValue(RectProperty); } set { SetValue(RectProperty, value); } }

        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(HtmlRect));
        public double RadiusX { get { return (double)GetValue(RadiusXProperty); } set { SetValue(RadiusXProperty, value); } }

        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(HtmlRect));
        public double RadiusY { get { return (double)GetValue(RadiusYProperty); } set { SetValue(RadiusYProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("x", Rect.X.ToString());
            attributes.Add("y", Rect.Y.ToString());
            attributes.Add("width", Rect.Width.ToString());
            attributes.Add("height", Rect.Height.ToString());
            if (HasValue(RadiusXProperty) && RadiusX > 0)
                attributes.Add("rx", RadiusX.ToString());
            if (HasValue(RadiusYProperty) && RadiusY > 0)
                attributes.Add("ry", RadiusY.ToString());
            return attributes;
        }
    }
}
