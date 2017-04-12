using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web.Controls
{
    [ContentProperty("Children")]
    public class Panel : Control
    {
        public Panel()
        {
            _Children = new UIElementCollection(this);
        }

        private UIElementCollection _Children;
        public UIElementCollection Children { get { return _Children; } }
    }
}
