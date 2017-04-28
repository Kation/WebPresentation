using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    [TypeConverter(typeof(RectTypeConverter))]
    public struct Rect
    {
        public Rect(double x, double y, double width, double height) : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(double width, double height) : this(0, 0, width, height) { }

        public double X, Y, Width, Height;

        public bool HasValue
        {
            get
            {
                return Width != 0 || Height != 0 || X != 0 || Y != 0;
            }
        }

        public string ToString(string spliter)
        {
            return X + spliter + Y + spliter + Width + spliter + Height;
        }

        public override string ToString()
        {
            return "{" + ToString(", ") + "}";
        }
    }

    public class RectTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else if (sourceType == typeof(Rect))
                return true;
            else
                return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return new Rect();
            else if (value is string)
            {
                string[] originValues = ((string)value).Split(',');
                if (originValues.Length != 4)
                    throw new NotSupportedException("不支持的格式。");
                try
                {
                    double[] values = originValues.Select(t => double.Parse(t)).ToArray();
                    return new Rect(values[0], values[1], values[2], values[3]);
                }
                catch
                {
                    throw new NotSupportedException("不支持的格式。");
                }
            }
            else if (value is Rect)
                return value;
            throw new NotSupportedException("不支持的格式。");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return ((Rect)value).ToString(",");
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
