using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Map : Navigator
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(Visualization));
        public string Data { get { return (string)GetValue(DataProperty); } set { SetValue(DataProperty, value); } }
    }
}
