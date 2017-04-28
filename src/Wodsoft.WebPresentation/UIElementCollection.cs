using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class UIElementCollection : ICollection<UIElement>, ICollection
    {
        private VisualCollection _Collection;
        private UIElement _LogicalParent;

        public UIElementCollection(UIElement parent)
        {
            _Collection = new VisualCollection(parent);
            _LogicalParent = parent;
        }
        
        public int Count { get { return _Collection.Count; } }

        public UIElement this[int index]
        {
            get
            {
                return (UIElement)_Collection[index];
            }
            set
            {
                _Collection[index] = value;
            }
        }

        bool ICollection.IsSynchronized { get { return ((ICollection)_Collection).IsSynchronized; } }

        bool ICollection<UIElement>.IsReadOnly { get { return ((ICollection<Visual>)_Collection).IsReadOnly; } }

        object ICollection.SyncRoot { get { return ((ICollection)_Collection).SyncRoot; } }

        public void Add(UIElement item)
        {
            _Collection.Add(item);
            if (_LogicalParent != null)
                _LogicalParent.AddLogicalChild(item);
        }

        public void Clear()
        {
            if (_LogicalParent != null)
                foreach (UIElement item in _Collection)
                    _LogicalParent.RemoveLogicalChild(item);
            _Collection.Clear();
        }

        public bool Contains(UIElement item) { return _Collection.Contains(item); }

        public void CopyTo(UIElement[] array, int arrayIndex) { _Collection.CopyTo(array, arrayIndex); }

        public bool Remove(UIElement item)
        {
            bool result = _Collection.Remove(item);
            if (result && _LogicalParent != null)
                _LogicalParent.RemoveLogicalChild(item);
            return result;
        }

        void ICollection.CopyTo(Array array, int index) { ((ICollection)_Collection).CopyTo(array, index); }

        public IEnumerator<UIElement> GetEnumerator()
        {
            return new EnumeratorWrapper(_Collection.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator() { return _Collection.GetEnumerator(); }

        private class EnumeratorWrapper : IEnumerator<UIElement>
        {
            private IEnumerator<Visual> _Enumerator;

            public EnumeratorWrapper(IEnumerator<Visual> enumerator)
            {
                _Enumerator = enumerator;
            }

            public UIElement Current { get { return _Enumerator.Current as UIElement; } }

            object IEnumerator.Current { get { return _Enumerator.Current; } }

            public void Dispose() { _Enumerator.Dispose(); }

            public bool MoveNext()
            {
                return _Enumerator.MoveNext();
            }

            public void Reset()
            {
                _Enumerator.Reset();
            }
        }
    }
}
