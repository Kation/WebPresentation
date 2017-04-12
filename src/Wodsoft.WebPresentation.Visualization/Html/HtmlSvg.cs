using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlSvg : HtmlContainer
    {
        public override string Tag { get { return "svg"; } }
        
        public static readonly DependencyProperty ViewBoxProperty = DependencyProperty.Register("ViewBox", typeof(Rect), typeof(HtmlSvg));
        public Rect ViewBox { get { return (Rect)GetValue(ViewBoxProperty); } set { SetValue(ViewBoxProperty, value); } }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(HtmlSvg));
        public double Width { get { return (double)GetValue(WidthProperty); } set { SetValue(WidthProperty, value); } }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(HtmlSvg));
        public double Height { get { return (double)GetValue(HeightProperty); } set { SetValue(HeightProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            attributes.Add("width", Width.ToString());
            attributes.Add("height", Height.ToString());
            if (HasValue(ViewBoxProperty) && ViewBox.HasValue)
                attributes.Add("viewBox", ViewBox.ToString(","));
            return attributes;
        }
    }
}
