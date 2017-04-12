using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Controls;

namespace Wodsoft.Web.Media
{
    public class Shape : Control
    {
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Color), typeof(Shape));
        public Color Fill { get { return (Color)GetValue(FillProperty); } set { SetValue(FillProperty, value); } }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Color), typeof(Shape));
        public Color Stroke { get { return (Color)GetValue(StrokeProperty); } set { SetValue(StrokeProperty, value); } }

        public static readonly DependencyProperty StrokeWidthProperty = DependencyProperty.Register("StrokeWidth", typeof(double), typeof(Shape));
        public double StrokeWidth { get { return (double)GetValue(StrokeWidthProperty); } set { SetValue(StrokeWidthProperty, value); } }
    }
}
