using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class TextBlock : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBlock));
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }

        public override void OnRender(RenderContext context)
        {
            if (Text != null)
                context.Writer.WriteString(Text);
        }
    }
}
