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
            else
            {
                FrameworkElement templatedParent = null;
                Visual visual = this;
                while (visual != null)
                {
                    if (visual is FrameworkElement)
                    {
                        templatedParent = ((FrameworkElement)visual)._TemplatedParent;
                        if (templatedParent != null)
                            break;
                    }
                    visual = visual.VisualParent;
                }
                if (templatedParent == null)
                    return;
                if (templatedParent is Panel)
                {
                    Panel panel = (Panel)templatedParent;
                    foreach(var element in panel.Children)
                    {
                        element.OnRender(context);
                    }
                }
                else
                {
                    var obj = templatedParent.GetValue(ContentControl.ContentProperty);
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
