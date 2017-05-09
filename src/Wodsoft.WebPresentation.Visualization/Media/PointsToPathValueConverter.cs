using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Data;

namespace Wodsoft.Web.Media
{
    public class PointsToPathValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new NotSupportedException();
            var points = value as IEnumerable<Point>;
            if (points == null)
                throw new ArgumentException("值不是Point枚举类型。", nameof(value));

            StringBuilder sb = new StringBuilder();
            foreach (var point in points)
            {
                if (sb.Length == 0)
                    sb.AppendFormat("M{0},{1}", point.X, point.Y);
                else
                    sb.AppendFormat("L{0},{1}", point.X, point.Y);
            }
            sb.AppendFormat(" Z");
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
