using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public sealed class VisualCollection : Collection<Visual>
    {
        private Visual _Parent;

        public VisualCollection(Visual parent)
        {
            _Parent = parent;
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
                _Parent.InternalRemoveVisualChild(item);
            base.ClearItems();
        }

        protected override void InsertItem(int index, Visual item)
        {
            if (item == null)
                return;
            _Parent.InternalAddVisualChild(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Visual item = this[index];
            _Parent.InternalRemoveVisualChild(item);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Visual item)
        {
            Visual old = this[index];
            _Parent.InternalRemoveVisualChild(old);
            _Parent.InternalAddVisualChild(item);
            base.SetItem(index, item);
        }
    }
}
