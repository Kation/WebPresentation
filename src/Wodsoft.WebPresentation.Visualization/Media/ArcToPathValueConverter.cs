using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Data;

namespace Wodsoft.Web.Media
{
    public class ArcToPathValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new NotSupportedException();
            Arc arc = value as Arc;
            if (arc == null)
                throw new ArgumentException("值不是Arc类型。", nameof(value));

            Point startInnerPoint = new Point(arc.Point.X + Math.Cos(ToArc(arc.StartAngle)) * arc.InnerRadius, arc.Point.Y - Math.Sin(ToArc(arc.StartAngle)) * arc.InnerRadius);
            Point startOuterPoint = new Point(arc.Point.X + Math.Cos(ToArc(arc.StartAngle)) * arc.OuterRadius, arc.Point.Y - Math.Sin(ToArc(arc.StartAngle)) * arc.OuterRadius);
            Point endInnerPoint = new Point(arc.Point.X + Math.Cos(ToArc(arc.EndAngle)) * arc.InnerRadius, arc.Point.Y - Math.Sin(ToArc(arc.EndAngle)) * arc.InnerRadius);
            Point endOuterPoint = new Point(arc.Point.X + Math.Cos(ToArc(arc.EndAngle)) * arc.OuterRadius, arc.Point.Y - Math.Sin(ToArc(arc.EndAngle)) * arc.OuterRadius);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("M{0},{1}", startInnerPoint.X, startInnerPoint.Y);
            sb.AppendFormat(" L{0},{1}", startOuterPoint.X, startOuterPoint.Y);
            sb.AppendFormat(" A{0},{0},{1},{2},{3},{4},{5}", arc.OuterRadius, 0, 0, 0, endOuterPoint.X, endOuterPoint.Y);
            if (arc.InnerRadius > 0)
            {
                sb.AppendFormat(" L{0},{1}", endInnerPoint.X, endInnerPoint.Y);
                sb.AppendFormat(" A{0},{0},{1},{2},{3},{4},{5}", arc.InnerRadius, 0, 0, 1, startInnerPoint.X, startInnerPoint.Y);
            }
            sb.AppendFormat(" Z");
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private double ToArc(double angle)
        {
            return angle / 180 * Math.PI;
        }
    }
}
