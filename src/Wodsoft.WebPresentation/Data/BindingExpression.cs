using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Reflection;

namespace Wodsoft.Web.Data
{
    public class BindingExpression : BindingExpressionBase
    {
        public string ElementName { get; set; }

        public object Source { get; set; }

        public string Path { get; set; }

        public override object GetValueCore(DependencyObject d, DependencyProperty dp)
        {
            if (Source != null)
                return GetValueCore(Source);
            if (ElementName == null)
                if (d is FrameworkElement)
                {
                    FrameworkElement element = (FrameworkElement)d;
                    while (element != null)
                    {
                        if (element.DataContext != null)
                            return GetValueCore(element.DataContext);
                        element = VisualHelper.GetParent(element) as FrameworkElement;
                    }
                    return null;
                }
                else
                    return null;
            if ((d is Visual) && dp == NameScope.NameScopeProperty)
                return null;
            INameScope nameScope = NameScope.GetNameScope(d);
            if (nameScope == null)
                return null;
            object source = nameScope.FindName(ElementName);
            return GetValueCore(source);
        }

        private object GetValueCore(object source)
        {
            if (Path == null)
                return source;
            string[] split = Path.Split('.');
            object value = source;
            for (int i = 0; i < split.Length; i++)
            {
                if (value == null)
                    return null;
                string property = split[i];
                //DependencyProperty
                if (property.StartsWith("(") && property.EndsWith(")"))
                {
                    if (!(value is DependencyObject))
                        return null;
                    if (!property.Contains('.'))
                        return null;
                    property = property.Substring(1, property.Length - 2);
                    string[] dop = property.Split('.');
                    if (dop.Length != 2)
                        return null;
                    Type ownerType = Type.GetType(dop[0]);
                    if (ownerType == null)
                        return null;
                    DependencyProperty dp = DependencyProperty.FromName(dop[1], ownerType);
                    if (dp == null)
                        return null;
                    DependencyObject d = (DependencyObject)value;
                    value = d.GetValue(dp);
                    continue;
                }
                var propertyInfo = value.GetType().GetProperty(property);
                if (propertyInfo == null)
                    return null;
                value = propertyInfo.GetValue(value);
            }
            return value;
        }
    }
}
