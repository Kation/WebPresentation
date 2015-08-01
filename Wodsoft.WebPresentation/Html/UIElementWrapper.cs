using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Html
{
    [ContentProperty("Content")]
    public class UIElementWrapper : HtmlElement
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(UIElement), typeof(UIElementWrapper), new PropertyMetadata(Visual.OnVisualChildPropertyChanged));
        public UIElement Content { get { return (UIElement)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }


        public override string Tag
        {
            get { return null; }
        }

        protected override void OnRenderContent(RenderContext context)
        {
            if (Content != null)
                Content.OnRender(context);
        }
    }
}
