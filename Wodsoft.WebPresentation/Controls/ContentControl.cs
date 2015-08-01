using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Controls
{
    [ContentProperty("Content")]
    public class ContentControl : Control
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ContentControl), new PropertyMetadata(Visual.OnVisualChildPropertyChanged));
        public object Content { get { return GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }
    }
}
