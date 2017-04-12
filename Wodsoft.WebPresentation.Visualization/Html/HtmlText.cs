using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlText : HtmlSvgSharp
    {
        public override string Tag { get { return "text"; } }

        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(HtmlText));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(HtmlText));
        public string Content { get { return (string)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("x", Point.X.ToString());
            attributes.Add("y", Point.Y.ToString());
            if (Content != null)
                attributes.Add("text", Content);

            return attributes;
        }
    }
}
