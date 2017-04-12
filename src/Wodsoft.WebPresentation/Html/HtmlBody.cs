using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    [TypeConverter(typeof(HtmlBodyConverter))]
    public class HtmlBody : HtmlContainer
    {
        public override string Tag
        {
            get { return "body"; }
        }
    }
}
