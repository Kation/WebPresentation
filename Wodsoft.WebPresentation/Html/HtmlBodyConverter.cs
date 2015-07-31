using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlBodyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(HtmlElement);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (!(value is HtmlElement))
                throw new NotSupportedException("Not support.");
            if (value is HtmlBody)
                return value;
            HtmlBody body = new HtmlBody();
            body.Children.Add((HtmlElement)value);
            return body;
        }
    }
}
