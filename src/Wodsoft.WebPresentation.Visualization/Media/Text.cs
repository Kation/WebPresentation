using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Text : Shape
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(Text));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(Text));
        public string Content { get { return (string)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }
    }
}
