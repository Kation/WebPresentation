using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public abstract class Expression
    {
        public virtual object GetValue(DependencyObject d, DependencyProperty dp)
        {
            return dp.DefaultMetadata.DefaultValue;
        }
    }
}
