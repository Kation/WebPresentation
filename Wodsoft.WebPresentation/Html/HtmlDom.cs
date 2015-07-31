using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public abstract class HtmlDom : HtmlElement
    {
        public static readonly DependencyProperty CssClassProperty = DependencyProperty.Register("CssClass", typeof(string), typeof(HtmlDom));
        public string CssClass { get { return (string)GetValue(CssClassProperty); } set { SetValue(CssClassProperty, value); } }

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(string), typeof(HtmlDom));
        public string Id { get { return (string)GetValue(IdProperty); } set { SetValue(IdProperty, value); } }

    }
}
