using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class DependencyPropertyChangedEventArgs : EventArgs
    {
        public DependencyPropertyChangedEventArgs(DependencyProperty dp, object oldValue, object newValue)
        {
            if (dp == null)
                throw new NullReferenceException("dp");
            Property = dp;
            NewValue = newValue;
            OldValue = oldValue;
        }

        public object NewValue { get; private set; }
        public object OldValue { get; private set; }
        public DependencyProperty Property { get; set; }
    }
}
