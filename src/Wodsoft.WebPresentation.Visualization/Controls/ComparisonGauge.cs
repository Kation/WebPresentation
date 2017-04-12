using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class ComparisonGauge : Gauge
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(decimal), typeof(ComparisonGauge));
        public decimal Target { get { return (decimal)GetValue(TargetProperty); } set { SetValue(TargetProperty, value); } }
    }
}
