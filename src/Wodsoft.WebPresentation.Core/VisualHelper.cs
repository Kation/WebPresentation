using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public static class VisualHelper
    {
        public static int GetChildrenCount(Visual visual)
        {
            return visual.InternalVisualChildrenCount;
        }

        public static Visual GetChild(Visual visual , int index)
        {
            return visual.InternalGetVisualChild(index);
        }

        public static Visual GetParent(Visual visual)
        {
            return visual.InternalVisualParent;
        }
    }
}
