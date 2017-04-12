using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public abstract class HtmlSvgElement : HtmlDom
    {
        public HtmlSvgElement()
        {
            _Children = new HtmlSvgElementCollection(this);
        }

        private HtmlSvgElementCollection _Children;
        public HtmlSvgElementCollection Children { get { return _Children; } }

        protected override int VisualChildrenCount { get { return _Children.Count; } }

        protected override Visual GetVisualChild(int index)
        {
            return _Children[index];
        }

        protected override void OnRenderContent(RenderContext context)
        {
            base.OnRenderContent(context);
            foreach (var item in Children)
                item.OnRender(context);
        }
    }
}
