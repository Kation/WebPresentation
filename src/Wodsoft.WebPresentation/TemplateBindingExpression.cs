using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Data;

namespace Wodsoft.Web
{
    public class TemplateBindingExpression : BindingExpressionBase
    {
        public TemplateBindingExpression(DependencyProperty property)
        {
            Property = property;
        }
        
        public DependencyProperty Property { get; set; }

        public override object GetValueCore(DependencyObject d, DependencyProperty dp)
        {
            UIElement element = d as UIElement;
            if (element == null)
                return null;
            FrameworkElement fe = LogicalTreeHelper.FindLogicalRoot(element) as FrameworkElement;
            if (fe == null || fe._TemplatedParent == null)
                return null;
            object value;
            if (Property == null)
                value = fe._TemplatedParent;
            else 
                value = fe._TemplatedParent.GetValue(Property);
            return value;
        }
    }
}
