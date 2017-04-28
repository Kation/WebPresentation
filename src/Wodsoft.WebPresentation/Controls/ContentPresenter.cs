using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class ContentPresenter : FrameworkElement
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ContentPresenter));
        public object Content { get { return GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }

        public override void OnRender(RenderContext context)
        {
            if (Content != null)
            {
                if (Content is UIElement)
                    ((UIElement)Content).OnRender(context);
                else
                    context.Writer.WriteString(Content.ToString());
            }
            UIElement element = LogicalTreeHelper.FindLogicalRoot(this);
            if (element is FrameworkElement)
            {
                FrameworkElement fe = (FrameworkElement)element;
                element = fe._TemplatedParent;
            }
            else
            {
                element = VisualHelper.GetParent(element) as UIElement;
            }
            if (element != null)
            {
                if (element is Panel)
                {
                    Panel panel = (Panel)element;
                    foreach (UIElement ui in panel.Children)
                    {
                        ui.OnRender(context);
                    }
                }
                else
                {
                    var obj = element.GetValue(ContentControl.ContentProperty);
                    if (obj != null)
                    {
                        if (obj is UIElement)
                            ((UIElement)obj).OnRender(context);
                        else
                            context.Writer.WriteString(obj.ToString());
                    }
                }
            }
        }
    }
}
