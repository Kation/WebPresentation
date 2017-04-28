using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.AspNetCore.Mvc
{
    public abstract class MvcBindingBase : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
