using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;

namespace Wodsoft.Web.Data
{
    public abstract class BindingBase : MarkupExtension
    {
        public IValueConverter Converter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public object ConverterParameter { get; set; }

        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            object obj = target.TargetObject;
            object property = target.TargetProperty;
                        
            DependencyObject dependencyObject = obj as DependencyObject;
            DependencyProperty dependencyProperty = null;
            if (property is PropertyInfo)
            {
                dependencyProperty = DependencyProperty.FromName(((PropertyInfo)property).Name, ((PropertyInfo)property).DeclaringType);
            }
            else if (property is DependencyProperty)
            {
                dependencyProperty = (DependencyProperty)property;
            }
            else if (property is MethodInfo)
            {
                MethodInfo method = (MethodInfo)property;
                dependencyProperty = DependencyProperty.FromName(method.Name.Substring(3), method.DeclaringType);                
            }
            return GetExpression(dependencyObject, dependencyProperty, serviceProvider);
        }

        protected internal abstract BindingExpressionBase GetExpression(DependencyObject d, DependencyProperty dp, IServiceProvider serviceProvider);

        public override string ToString()
        {
            return "{BindingBase}";
        }
    }
}
