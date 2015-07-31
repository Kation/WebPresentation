using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web
{
    public class NameScope : INameScopeDictionary, INameScope, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        private class Enumerator : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
        {
            private IDictionaryEnumerator _enumerator;
            public KeyValuePair<string, object> Current
            {
                get
                {
                    if (this._enumerator == null)
                    {
                        return default(KeyValuePair<string, object>);
                    }
                    return new KeyValuePair<string, object>((string)this._enumerator.Key, this._enumerator.Value);
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
            public Enumerator(HybridDictionary nameMap)
            {
                this._enumerator = null;
                if (nameMap != null)
                {
                    this._enumerator = nameMap.GetEnumerator();
                }
            }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
            public bool MoveNext()
            {
                return this._enumerator != null && this._enumerator.MoveNext();
            }
            void IEnumerator.Reset()
            {
                if (this._enumerator != null)
                {
                    this._enumerator.Reset();
                }
            }
        }
        public static readonly DependencyProperty NameScopeProperty = DependencyProperty.RegisterAttached("NameScope", typeof(INameScope), typeof(NameScope));
        private HybridDictionary _nameMap;
        public int Count
        {
            get
            {
                if (this._nameMap == null)
                {
                    return 0;
                }
                return this._nameMap.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public object this[string key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                return this.FindName(key);
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.RegisterName(key, value);
            }
        }
        public ICollection<string> Keys
        {
            get
            {
                if (this._nameMap == null)
                {
                    return null;
                }
                List<string> list = new List<string>();
                foreach (string item in this._nameMap.Keys)
                {
                    list.Add(item);
                }
                return list;
            }
        }
        public ICollection<object> Values
        {
            get
            {
                if (this._nameMap == null)
                {
                    return null;
                }
                List<object> list = new List<object>();
                foreach (object current in this._nameMap.Values)
                {
                    list.Add(current);
                }
                return list;
            }
        }
        public void RegisterName(string name, object scopedElement)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (scopedElement == null)
            {
                throw new ArgumentNullException("scopedElement");
            }
            if (name == string.Empty)
            {
                throw new ArgumentNullException();
            }
            if (this._nameMap == null)
            {
                this._nameMap = new HybridDictionary();
                this._nameMap[name] = scopedElement;
            }
            else
            {
                object obj = this._nameMap[name];
                if (obj == null)
                {
                    this._nameMap[name] = scopedElement;
                }
                else
                {
                    if (scopedElement != obj)
                    {
                        throw new ArgumentException("NameScopeDuplicateNamesNotAllowed");
                    }
                }
            }
        }
        public void UnregisterName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name == string.Empty)
            {
                throw new ArgumentException("NameScopeNameNotEmptyString");
            }
            if (this._nameMap != null && this._nameMap[name] != null)
            {
                this._nameMap.Remove(name);
                return;
            }
            throw new ArgumentException("NameScopeNameNotFound");
        }
        public object FindName(string name)
        {
            if (this._nameMap == null || name == null || name == string.Empty)
            {
                return null;
            }
            return this._nameMap[name];
        }
        internal static INameScope NameScopeFromObject(object obj)
        {
            INameScope nameScope = null;
            while (obj != null)
            {
                nameScope = obj as INameScope;
                if (nameScope == null)
                {
                    DependencyObject dependencyObject = obj as DependencyObject;
                    if (dependencyObject != null)
                    {
                        nameScope = NameScope.GetNameScope(dependencyObject);
                        break;
                    }
                    if (obj is Visual)
                        obj = ((Visual)obj).VisualParent;
                    else
                        break;
                }
            }
            return nameScope;
        }
        public static void SetNameScope(DependencyObject dependencyObject, INameScope value)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }
            dependencyObject.SetValue(NameScope.NameScopeProperty, value);
        }
        public static INameScope GetNameScope(DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }
            return (INameScope)dependencyObject.GetValue(NameScope.NameScopeProperty);
        }
        private IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return new NameScope.Enumerator(this._nameMap);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public void Clear()
        {
            this._nameMap = null;
        }
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (this._nameMap == null)
            {
                array = null;
                return;
            }
            foreach (DictionaryEntry dictionaryEntry in this._nameMap)
            {
                array[arrayIndex++] = new KeyValuePair<string, object>((string)dictionaryEntry.Key, dictionaryEntry.Value);
            }
        }
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.Contains(item) && item.Value == this[item.Key] && this.Remove(item.Key);
        }
        public void Add(KeyValuePair<string, object> item)
        {
            if (item.Key == null)
            {
                throw new ArgumentException("ReferenceIsNull");
            }
            if (item.Value == null)
            {
                throw new ArgumentException("ReferenceIsNull");
            }
            this.Add(item.Key, item.Value);
        }
        public bool Contains(KeyValuePair<string, object> item)
        {
            if (item.Key == null)
            {
                throw new ArgumentException("ReferenceIsNull");
            }
            return this.ContainsKey(item.Key);
        }
        public void Add(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            this.RegisterName(key, value);
        }
        public bool ContainsKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            object obj = this.FindName(key);
            return obj != null;
        }
        public bool Remove(string key)
        {
            if (!this.ContainsKey(key))
            {
                return false;
            }
            this.UnregisterName(key);
            return true;
        }
        public bool TryGetValue(string key, out object value)
        {
            if (!this.ContainsKey(key))
            {
                value = null;
                return false;
            }
            value = this.FindName(key);
            return true;
        }
        public NameScope()
        {
        }
    }
}
