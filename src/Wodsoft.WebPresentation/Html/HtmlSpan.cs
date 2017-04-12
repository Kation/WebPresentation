using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlSpan : HtmlElement
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HtmlSpan));
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }

        public override string Tag
        {
            get { return "span"; }
        }

        protected override void OnRenderContent(RenderContext context)
        {
            if (Text != null)
                context.Writer.WriteString(Text);
        }
    }
}
