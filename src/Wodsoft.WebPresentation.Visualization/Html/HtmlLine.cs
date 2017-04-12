using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlLine : HtmlSvgSharp
    {
        public override string Tag { get { return "line"; } }

        public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(Point), typeof(HtmlLine));
        public Point Start { get { return (Point)GetValue(StartProperty); } set { SetValue(StartProperty, value); } }

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register("End", typeof(Point), typeof(HtmlLine));
        public Point End { get { return (Point)GetValue(EndProperty); } set { SetValue(EndProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("x1", Start.X.ToString());
            attributes.Add("y1", Start.Y.ToString());
            attributes.Add("x2", End.X.ToString());
            attributes.Add("y2", End.Y.ToString());

            return attributes;
        }
    }
}
