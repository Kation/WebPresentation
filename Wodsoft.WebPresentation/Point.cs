using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public struct Point
    {
        public Point(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        public double X, Y;

        public override string ToString()
        {
            return "{" + ToString(", ") + "}";
        }

        public string ToString(string spliter)
        {
            return X + spliter + Y;
        }
    }

    public class PointTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else if (sourceType == typeof(Point))
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
                if (originValues.Length != 2)
                    throw new NotSupportedException("不支持的格式。");
                try
                {
                    double[] values = originValues.Select(t => double.Parse(t)).ToArray();
                    return new Point(values[0], values[1]);
                }
                catch
                {
                    throw new NotSupportedException("不支持的格式。");
                }
            }
            else if (value is Point)
                return value;
            throw new NotSupportedException("不支持的格式。");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return ((Point)value).ToString(",");
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
