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
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ContentControl), new PropertyMetadata(OnContentPropertyChanged));
        public object Content { get { return GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }
        private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContentControl control = (ContentControl)d;
            if (e.OldValue != null)
            {
                if (e.OldValue is UIElement)
                {
                    control.RemoveLogicalChild((UIElement)e.OldValue);
                }
            }
            if (e.NewValue != null)
            {
                if (e.NewValue is UIElement)
                {
                    control.AddLogicalChild((UIElement)e.NewValue);
                }
            }
        }
    }
}
