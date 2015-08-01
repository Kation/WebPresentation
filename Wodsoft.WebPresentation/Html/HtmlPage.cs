using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Html
{
    [ContentProperty("Body")]
    public class HtmlPage : HtmlElement
    {
        public static readonly DependencyProperty HeadProperty = DependencyProperty.Register("Head", typeof(HtmlHead), typeof(HtmlPage), new PropertyMetadata(Visual.OnVisualChildPropertyChanged));
        public HtmlHead Head { get { return (HtmlHead)GetValue(HeadProperty); } set { SetValue(HeadProperty, value); } }

        public static readonly DependencyProperty BodyProperty = DependencyProperty.Register("Body", typeof(HtmlBody), typeof(HtmlPage), new PropertyMetadata(new HtmlBody(), Visual.OnVisualChildPropertyChanged));
        public HtmlBody Body { get { return (HtmlBody)GetValue(BodyProperty); } set { SetValue(BodyProperty, value); } }

        protected override int VisualChildrenCount
        {
            get
            {
                int count = 0;
                if (Head != null)
                    count++;
                if (Body != null)
                    count++;
                return count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0)
            {
                if (Head != null)
                    return Head;
                if (Body != null)
                    return Body;
                throw new ArgumentOutOfRangeException("index");
            }
            if (index == 1)
            {
                if (Head == null && Body == null)
                    throw new ArgumentOutOfRangeException("index");
                return Body;
            }
            throw new ArgumentOutOfRangeException("index");
        }

        public override string Tag
        {
            get { return "html"; }
        }

        protected override void OnRenderContent(RenderContext context)
        {
            if (Head != null)
                Head.OnRender(context);
            if (Body != null)
                Body.OnRender(context);
        }
    }
}
