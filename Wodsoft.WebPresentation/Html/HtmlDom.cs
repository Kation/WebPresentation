using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public abstract class HtmlDom : HtmlElement
    {
        public HtmlDom()
        {
            Style = new HtmlStyleCollection();
        }

        public static readonly DependencyProperty CssClassProperty = DependencyProperty.Register("CssClass", typeof(string), typeof(HtmlDom));
        public string CssClass { get { return (string)GetValue(CssClassProperty); } set { SetValue(CssClassProperty, value); } }

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(string), typeof(HtmlDom));
        public string Id { get { return (string)GetValue(IdProperty); } set { SetValue(IdProperty, value); } }

        public HtmlStyleCollection Style { get; private set; }

        protected override void OnRenderContent(RenderContext context)
        {
            var value = "";
            foreach (var key in Style.Keys)
            {
                if (value.Length > 0)
                    value += "; ";
                value += key + ": " + Style[key];
            }
            context.Writer.WriteAttributeString("style", value);
        }
    }
}
