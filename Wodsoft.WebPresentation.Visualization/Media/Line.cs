using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Line : Shape
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(Point), typeof(Line));
        public Point Start { get { return (Point)GetValue(StartProperty); } set { SetValue(StartProperty, value); } }

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register("End", typeof(Point), typeof(Line));
        public Point End { get { return (Point)GetValue(EndProperty); } set { SetValue(EndProperty, value); } }
    }
}
