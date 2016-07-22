using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlRect : HtmlSvgSharp
    {
        public override string Tag { get { return "rect"; } }
        
        public static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(HtmlRect));
        public Rect Rect { get { return (Rect)GetValue(RectProperty); } set { SetValue(RectProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("x", Rect.X.ToString());
            attributes.Add("y", Rect.Y.ToString());
            attributes.Add("width", Rect.Width.ToString());
            attributes.Add("height", Rect.Height.ToString());
            
            return attributes;
        }
    }
}
