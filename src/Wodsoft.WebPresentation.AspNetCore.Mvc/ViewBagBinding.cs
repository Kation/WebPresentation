using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wodsoft.Web.Data;

namespace Wodsoft.Web.AspNetCore.Mvc
{
    public class ViewBagBinding : BindingBase
    {
        public ViewBagBinding(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            Name = name;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        protected override BindingExpressionBase GetExpression(DependencyObject d, DependencyProperty dp, IServiceProvider serviceProvider)
        {
            return null;
        }
    }
}
