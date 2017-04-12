using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Canvas : Panel
    {
        public static readonly DependencyProperty ViewBoxProperty = DependencyProperty.Register("ViewBox", typeof(Rect), typeof(Canvas));
        public Rect ViewBox { get { return (Rect)GetValue(ViewBoxProperty); } set { SetValue(ViewBoxProperty, value); } }


    }
}
