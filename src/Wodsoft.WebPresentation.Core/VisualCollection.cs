using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public abstract class VisualCollection<T> : Collection<T>
        where T : Visual
    {
        protected Visual Parent { get; private set; }

        protected VisualCollection(Visual parent)
        {
            Parent = parent;
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
                Parent.RemoveVisualChild(item);
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            if (item == null)
                return;
            Parent.AddVisualChild(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Visual item = this[index];
            Parent.RemoveVisualChild(item);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            Visual old = this[index];
            Parent.RemoveVisualChild(old);
            Parent.AddVisualChild(item);
            base.SetItem(index, item);
        }
    }
}
