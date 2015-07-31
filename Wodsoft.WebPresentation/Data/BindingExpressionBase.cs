using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Data
{
    public abstract class BindingExpressionBase : Expression
    {
        public IValueConverter Converter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public object ConverterParameter { get; set; }

        public sealed override object GetValue(DependencyObject d, DependencyProperty dp)
        {
            object value = GetValueCore(d, dp);
            if (Converter != null)
                value = Converter.Convert(value, dp.PropertyType, ConverterParameter, ConverterCulture);
            if (value != null && !dp.PropertyType.IsAssignableFrom(value.GetType()))
                value = null;
            return value;
        }

        public abstract object GetValueCore(DependencyObject d, DependencyProperty dp);
    }
}
