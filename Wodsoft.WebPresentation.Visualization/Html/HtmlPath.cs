using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlPath : HtmlSvgSharp
    {
        public override string Tag { get { return "path"; } }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(HtmlPath));
        public string Data { get { return (string)GetValue(DataProperty); } set { SetValue(DataProperty, value); } }

        protected override NameValueCollection GetAttributes()
        {
            var attributes = base.GetAttributes();
            if (HasValue(DataProperty))
                attributes.Add("d", Data);

            return attributes;
        }
    }
}
