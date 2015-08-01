using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Wodsoft.Web.Html;

namespace Wodsoft.Web.Controls
{
    [ContentProperty("Content")]
    public class HtmlWrapper : FrameworkElement
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(HtmlElement), typeof(HtmlWrapper), new PropertyMetadata(Visual.OnVisualChildPropertyChanged));
        public HtmlElement Content { get { return (HtmlElement)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }

        public override void OnRender(RenderContext context)
        {
            if (Content != null)
                Content.OnRender(context);
        }
    }
}
