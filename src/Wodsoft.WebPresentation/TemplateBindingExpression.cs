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
        public TemplateBindingExpression(FrameworkElement root)
        {
            if (root == null)
                throw new ArgumentNullException("root");
            Root = root;
        }

        public FrameworkElement Root { get; private set; }

        public string Property { get; set; }

        public override object GetValueCore(DependencyObject d, DependencyProperty dp)
        {
            if (Root._TemplatedParent == null)
                throw new NotSupportedException("Template content not apply to any element.");
            UIElement element = Root._TemplatedParent;
            if (Property == null)
                return element;
            if (Property.Contains("."))
            {
                string[] dop = Property.Split('.');
                if (dop.Length != 2)
                    throw new ArgumentException("Property invalid.");
                Type ownerType = Type.GetType(dop[0]);
                if (ownerType == null)
                    return null;
                DependencyProperty property = DependencyProperty.FromName(dop[1], ownerType);
                return element.GetValue(property);
            }
            else
            {
                var propertyInfo = element.GetType().GetProperty(Property);
                if (propertyInfo == null)
                    return null;
                DependencyProperty property = DependencyProperty.FromName(Property, propertyInfo.DeclaringType);
                return element.GetValue(property);
            }
        }
    }
}
