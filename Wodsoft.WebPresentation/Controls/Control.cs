using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Control : FrameworkElement
    {
        public static readonly DependencyProperty TemplateProperty = DependencyProperty.Register("Template", typeof(ControlTemplate), typeof(Control));
        public ControlTemplate Template { get { return (ControlTemplate)GetValue(TemplateProperty); } set { SetValue(TemplateProperty, value); } }

        protected override FrameworkTemplate ElementTemplate
        {
            get
            {
                return Template;
            }
        }
    }
}
