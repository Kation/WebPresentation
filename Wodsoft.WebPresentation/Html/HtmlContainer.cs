using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public abstract class HtmlContainer : HtmlDom
    {
        public HtmlContainer()
        {
            _Children = new HtmlElementCollection(this);
        }

        private HtmlElementCollection _Children;
        public HtmlElementCollection Children { get { return _Children; } }

        protected override int VisualChildrenCount { get { return _Children.Count; } }

        protected override Visual GetVisualChild(int index)
        {
            return _Children[index];
        }

        protected override void OnRenderContent(RenderContext context)
        {
            foreach (var item in Children)
                item.OnRender(context);
        }
    }
}
