using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;
using Wodsoft.Web.Data;

namespace Wodsoft.Web
{
    public class TemplateBindingExtension : BindingBase
    {
        public TemplateBindingExtension()
        {

        }

        public TemplateBindingExtension(DependencyProperty property)
        {
            Property = property;
        }

        [ConstructorArgument("property")]
        public DependencyProperty Property { get; set; }

        protected internal override BindingExpressionBase GetExpression(DependencyObject d, DependencyProperty dp, IServiceProvider serviceProvider)
        {
            //IRootObjectProvider rootProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            //FrameworkElement root = rootProvider.RootObject as FrameworkElement;
            return new TemplateBindingExpression(Property) { Converter = Converter, ConverterCulture = ConverterCulture , ConverterParameter = ConverterParameter };
        }
    }
}
