using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Path : Shape
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(Path));
        public string Data { get { return (string)GetValue(DataProperty); } set { SetValue(DataProperty, value); } }
    }
}
