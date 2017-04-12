using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Range : Gauge
    {
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(decimal), typeof(Range));
        public decimal Maximum { get { return (decimal)GetValue(MaximumProperty); } set { SetValue(MaximumProperty, value); } }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(decimal), typeof(Range));
        public decimal Minimum { get { return (decimal)GetValue(MinimumProperty); } set { SetValue(MinimumProperty, value); } }
    }
}
