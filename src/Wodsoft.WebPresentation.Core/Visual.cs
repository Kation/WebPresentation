using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public abstract class Visual : DependencyObject
    {
        private Visual _Parent;

        protected virtual void AddVisualChild(Visual child)
        {
            if (child == null)
                return;
            if (child._Parent != null)
                throw new ArgumentException("Child has parent.");
            child._Parent = this;
        }

        protected virtual void RemoveVisualChild(Visual child)
        {
            if (child == null)
                return;
            if (child._Parent != this)
                throw new ArgumentException("Not child parent.");
            child._Parent = null;
        }

        protected virtual Visual GetVisualChild(int index)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        protected virtual int VisualChildrenCount { get { return 0; } }

        protected Visual VisualParent { get { return _Parent; } }
        
        internal void InternalAddVisualChild(Visual child)
        {
            AddVisualChild(child);
        }

        internal void InternalRemoveVisualChild(Visual child)
        {
            RemoveVisualChild(child);
        }

        internal Visual InternalGetVisualChild(int index)
        {
            return GetVisualChild(index);
        }

        internal int InternalVisualChildrenCount { get { return VisualChildrenCount; } }

        internal Visual InternalVisualParent { get { return VisualParent; } }

        public abstract void Render(TextWriter writer);

        public static void OnVisualChildPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Visual visual = (Visual)d;
            if (visual == null)
                return;
            Visual oldValue = e.OldValue as Visual;
            if (oldValue != null && oldValue.VisualParent == visual)
                visual.RemoveVisualChild(oldValue);
            Visual newValue = e.NewValue as Visual;
            if (newValue != null && newValue.VisualParent == null)
                visual.AddVisualChild(newValue);
        }
    }
}
