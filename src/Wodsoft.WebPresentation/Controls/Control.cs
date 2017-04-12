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

        public static readonly DependencyProperty TabIndexProperty = DependencyProperty.Register("TabIndex", typeof(int), typeof(Control), new PropertyMetadata(int.MaxValue));
        public int TabIndex { get { return (int)GetValue(TabIndexProperty); } set { SetValue(TabIndexProperty, value); } }
        
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(Control));
        public double Width { get { return (double)GetValue(WidthProperty); } set { SetValue(WidthProperty, value); } }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(Control));
        public double Height { get { return (double)GetValue(HeightProperty); } set { SetValue(HeightProperty, value); } }

        protected override FrameworkTemplate ElementTemplate
        {
            get
            {
                return Template;
            }
        }
    }
}
