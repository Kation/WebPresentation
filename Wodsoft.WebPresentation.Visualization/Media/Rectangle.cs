using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Rectangle : Shape
    {
        public static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(Rectangle));
        public Rect Rect { get { return (Rect)GetValue(RectProperty); } set { SetValue(RectProperty, value); } }        
    }
}
