using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public static class LogicalTreeHelper
    {
        public static IEnumerable GetChildren(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            var enumerator = element.LogicalChildren;
            return new EnumeratorWrapper(enumerator);
        }

        public static UIElement GetParent(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            return element.Parent;
        }

        public static UIElement FindLogicalRoot(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            while (element.Parent != null)
                element = element.Parent;
            return element;
        }

        private class EnumeratorWrapper : IEnumerable<UIElement>
        {
            private IEnumerator<UIElement> _enumerator;

            private static EnumeratorWrapper _emptyInstance;

            public EnumeratorWrapper(IEnumerator<UIElement> enumerator)
            {
                if (enumerator == null)
                    enumerator = EmptyEnumerator.Instance;
                _enumerator = enumerator;
            }

            public IEnumerator<UIElement> GetEnumerator()
            {
                return _enumerator;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _enumerator;
            }
        }

        private class EmptyEnumerator : IEnumerator<UIElement>
        {
            private static IEnumerator<UIElement> _instance;

            public static IEnumerator<UIElement> Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new EmptyEnumerator();
                    }
                    return _instance;
                }
            }

            public UIElement Current { get { throw new InvalidOperationException(); } }
            
            object IEnumerator.Current { get { throw new InvalidOperationException(); } }

            private EmptyEnumerator() { }

            public void Reset() { }

            public bool MoveNext() { return false; }

            public void Dispose() { }
        }
    }
}
