using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Visualization : Panel
    {
        public static readonly DependencyProperty ViewBoxProperty = DependencyProperty.Register("ViewBox", typeof(Rect), typeof(Visualization));
        public Rect ViewBox { get { return (Rect)GetValue(ViewBoxProperty); } set { SetValue(ViewBoxProperty, value); } }


    }
}
