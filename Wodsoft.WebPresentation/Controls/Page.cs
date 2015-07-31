using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Page : ContentControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Page));
        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }

        public override void OnRender(RenderContext context)
        {
            context.Writer.WriteDocType("html", null, null, null);
            base.OnRender(context);
        }
    }
}
