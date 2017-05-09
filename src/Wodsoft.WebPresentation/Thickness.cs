using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public struct Thickness
    {
        public Thickness(double uniformLength)
        {
            Bottom = uniformLength;
            Right = uniformLength;
            Top = uniformLength;
            Left = uniformLength;
        }

        public Thickness(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return (t1.Left == t2.Left || (double.IsNaN(t1.Left) && double.IsNaN(t2.Left))) && (t1.Top == t2.Top || (double.IsNaN(t1.Top) && double.IsNaN(t2.Top))) && (t1.Right == t2.Right || (double.IsNaN(t1.Right) && double.IsNaN(t2.Right))) && (t1.Bottom == t2.Bottom || (double.IsNaN(t1.Bottom) && double.IsNaN(t2.Bottom)));
        }

        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !(t1 == t2);
        }
        public override bool Equals(object obj)
        {
            if (obj is Thickness)
            {
                Thickness t = (Thickness)obj;
                return this == t;
            }
            return false;
        }

        public bool Equals(Thickness thickness)
        {
            return this == thickness;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode();
        }
    }
}
