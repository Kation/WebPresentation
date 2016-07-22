using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Media;

namespace Wodsoft.Web.Html
{
    public abstract class HtmlSvgSharp : HtmlSvgElement
    {
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Color?), typeof(HtmlSvgSharp));
        public Color? Fill { get { return (Color?)GetValue(FillProperty); } set { SetValue(FillProperty, value); } }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Color?), typeof(HtmlSvgSharp));
        public Color? Stroke { get { return (Color?)GetValue(StrokeProperty); } set { SetValue(StrokeProperty, value); } }

        public static readonly DependencyProperty StrokeWidthProperty = DependencyProperty.Register("StrokeWidth", typeof(double), typeof(HtmlSvgSharp));
        public double StrokeWidth { get { return (double)GetValue(StrokeWidthProperty); } set { SetValue(StrokeWidthProperty, value); } }

        protected override void OnRenderContent(RenderContext context)
        {
            if (HasValue(FillProperty))
                Style["fill"] = Fill.HasValue ? Fill.Value.ToCssString() : "none";
            if (HasValue(StrokeProperty))
                Style["stroke"] = Stroke.HasValue ? Stroke.Value.ToCssString() : "none";
            if (HasValue(StrokeProperty))
                Style["stroke-width"] = StrokeWidth.ToString();
            base.OnRenderContent(context);
        }
    }
}
