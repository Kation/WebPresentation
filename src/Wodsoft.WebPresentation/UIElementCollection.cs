using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class UIElementCollection : VisualCollection<UIElement>
    {
        public UIElementCollection(UIElement parent) : base(parent) { }
    }
}
