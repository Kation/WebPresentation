using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public static class VisualHelper
    {
        public static int GetVisualChildrenCount(Visual visual)
        {
            return visual.VisualChildrenCount;
        }

        public static Visual GetVisualChild(Visual visual , int index)
        {
            return visual.GetVisualChild(index);
        }
    }
}
