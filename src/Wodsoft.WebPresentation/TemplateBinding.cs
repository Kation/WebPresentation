using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Wodsoft.Web.Data;

namespace Wodsoft.Web
{
    public class TemplateBinding : BindingBase
    {
        public TemplateBinding()
        {

        }

        public TemplateBinding(string property)
        {
            Property = property;
        }

        public string Property { get; set; }

        protected internal override BindingExpressionBase GetExpression(DependencyObject d, DependencyProperty dp, IServiceProvider serviceProvider)
        {
            IRootObjectProvider rootProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            FrameworkElement root = rootProvider.RootObject as FrameworkElement;
            return new TemplateBindingExpression(root) { Property = Property };
        }
    }
}
