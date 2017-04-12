using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    [TypeConverter(typeof(ColorTypeConverter))]
    public struct Color
    {
        public Color(byte r, byte g, byte b, byte a) : this()
        {
            Red = r;
            Green = g;
            Blue = b;
            Alpha = a;
        }

        public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

        public byte Red, Green, Blue, Alpha;

        public string ToCssString()
        {
            string value = Red + "," + Green + "," + Blue;
            if (Alpha < 255)
                value = "rgba(" + value + "," + Alpha + ")";
            else
                value = "rgb(" + value + ")";
            return value;
        }

        public string ToString(string spliter)
        {
            string value = Red + spliter + Green + spliter + Blue;
            if (Alpha < 255)
                value += spliter + Alpha;
            return value;
        }

        public override string ToString()
        {
            return "{" + ToString(",") + "}";
        }
    }


    public class ColorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else if (sourceType == typeof(Color))
                return true;
            else
                return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return new Color();
            else if (value is string)
            {
                string[] originValues = ((string)value).Split(',');
                if (originValues.Length != 3 && originValues.Length != 4)
                    throw new NotSupportedException("不支持的格式。");
                try
                {
                    byte[] values = originValues.Select(t => byte.Parse(t)).ToArray();
                    if (values.Length == 3)
                        return new Color(values[0], values[1], values[2]);
                    else
                        return new Color(values[0], values[1], values[2], values[3]);
                }
                catch
                {
                    throw new NotSupportedException("不支持的格式。");
                }
            }
            else if (value is Color)
                return value;
            throw new NotSupportedException("不支持的格式。");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return ((Color)value).ToString(",");
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
