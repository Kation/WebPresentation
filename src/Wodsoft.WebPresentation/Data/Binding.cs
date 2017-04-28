using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Data
{
    public class Binding : BindingBase
    {
        public Binding()
        { }

        public Binding(string path)
        {
            Path = path;
        }

        public string ElementName { get; set; }

        public object Source { get; set; }

        [ConstructorArgument("path")]
        public string Path { get; set; }

        protected internal override BindingExpressionBase GetExpression(DependencyObject d, DependencyProperty dp, IServiceProvider serviceProvider)
        {
            BindingExpression expression = new BindingExpression();
            expression.ElementName = ElementName;
            expression.Source = Source;
            expression.Path = Path;
            expression.Converter = Converter;
            expression.ConverterCulture = ConverterCulture;
            expression.ConverterParameter = ConverterParameter;
            return expression;
        }
    }
}
