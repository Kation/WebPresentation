using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public sealed class DependencyPropertyKey
    {
        internal DependencyPropertyKey(DependencyProperty dp)
        {
            Property = dp;
        }

        public DependencyProperty Property { get; private set; }
    }
}
