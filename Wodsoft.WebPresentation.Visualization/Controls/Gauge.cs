using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Gauge : Visualization
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(decimal), typeof(Visualization));
        public decimal Value { get { return (decimal)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }
    }
}
