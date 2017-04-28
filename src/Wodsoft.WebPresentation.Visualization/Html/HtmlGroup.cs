using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Html
{
    [ContentProperty("Children")]
    public class HtmlGroup : HtmlSvgElement
    {
        public HtmlGroup()
        {
            _Children = new UIElementCollection(this);
        }

        public override string Tag { get { return "g"; } }

        private UIElementCollection _Children;
        public UIElementCollection Children { get { return _Children; } }

        protected override int VisualChildrenCount { get { return _Children.Count; } }

        protected override Visual GetVisualChild(int index)
        {
            return _Children[index];
        }

        protected override void OnRenderContent(RenderContext context)
        {
            base.OnRenderContent(context);
            foreach (UIElement item in Children)
                item.OnRender(context);
        }
    }
}
