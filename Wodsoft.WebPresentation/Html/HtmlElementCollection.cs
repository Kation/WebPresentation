using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlElementCollection<T> : VisualCollection<T>
        where T : HtmlElement
    {
        public HtmlElementCollection(HtmlElement element) : base(element) { }
    }
    
    public class HtmlElementCollection : HtmlElementCollection<HtmlElement>
    {
        public HtmlElementCollection(HtmlElement element) : base(element) { }
    }
}
