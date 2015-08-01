using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Html
{
    [ContentProperty("Children")]
    public abstract class HtmlElement : UIElement
    {
        public abstract string Tag { get; }

        public sealed override void OnRender(RenderContext context)
        {
            if (Tag == null)
            {
                OnRenderContent(context);
                return;
            }
            context.Writer.WriteStartElement(Tag);
            var attributes = GetAttributes();
            foreach (var key in GetAttributes().AllKeys)
                context.Writer.WriteAttributeString(key, attributes[key]);
            OnRenderContent(context);
            context.Writer.WriteEndElement();
        }

        protected virtual void OnRenderContent(RenderContext context)
        {

        }

        protected virtual NameValueCollection GetAttributes()
        {
            return new NameValueCollection();
        }
    }
}
