using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace Wodsoft.Web.Xaml
{
    public class ObjectMemberInvoker : XamlMemberInvoker
    {
        public ObjectMemberInvoker(DependencyProperty property)
        {
            Property = property;
        }

        public DependencyProperty Property { get; private set; }

        public override object GetValue(object instance)
        {
            DependencyObject d = (DependencyObject)instance;
            return d.GetValue(Property);
        }

        public override void SetValue(object instance, object value)
        {
            DependencyObject d = (DependencyObject)instance;
            d.SetValue(Property, value);
        }
    }
}
