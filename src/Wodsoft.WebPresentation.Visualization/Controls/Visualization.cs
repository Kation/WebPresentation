using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public abstract class Visualization : Control
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Visualization));
        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
    }
}
