using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.ComponentModel;
using System.IO;

namespace Wodsoft.Web
{
    [Ambient]
    public class ResourceDictionary : IDictionary, ICollection, IEnumerable, INameScope, ISupportInitialize
    {
        private Dictionary<object, object> _Res;
        private NameScope _NameScope;
        private Uri _Source;

        public ResourceDictionary()
        {
            _Res = new Dictionary<object, object>();
            _NameScope = new NameScope();
            _MergedDictionaries = new Collection<ResourceDictionary>();
        }

        private Collection<ResourceDictionary> _MergedDictionaries;
        public Collection<ResourceDictionary> MergedDictionaries { get { return _MergedDictionaries; } }

        public Uri Source
        {
            get
            {
                return _Source;
            }
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value.OriginalString))
                    throw new ArgumentException("地址为空。");
                var stream = File.Open(value.OriginalString, FileMode.Open, FileAccess.Read, FileShare.Read);
                Xaml.XamlReader reader = new Xaml.XamlReader();
                ResourceDictionary res = reader.Load(stream) as ResourceDictionary;
                if (res != null)
                {
                    foreach (var obj in res._Res)
                        _Res.Add(obj.Key, obj.Value);
                    foreach (var obj in res._MergedDictionaries)
                        _MergedDictionaries.Add(obj);
                }
            }
        }

        public void Add(object key, object value)
        {
            if (_Res.ContainsKey(key))
                _Res[key] = value;
            else
                _Res.Add(key, value);
        }

        public void Clear()
        {
            _Res.Clear();
        }

        public bool Contains(object key)
        {
            bool result = _Res.ContainsKey(key);
            if (result)
                return true;
            foreach (var res in MergedDictionaries)
            {
                result = res.Contains(key);
                if (result)
                    return true;
            }
            return false;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return _Res.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ICollection Keys
        {
            get { return _Res.Keys; }
        }

        public void Remove(object key)
        {
            _Res.Remove(key);
        }

        public ICollection Values
        {
            get { return _Res.Values; }
        }

        public object this[object key]
        {
            get
            {
                if (_Res.ContainsKey(key))
                    return _Res[key];
                foreach (var res in _MergedDictionaries)
                    if (res.Contains(key))
                        return res[key];
                return null;
            }
            set
            {
                if (_Res.ContainsKey(key))
                    _Res[key] = value;
                else
                    _Res.Add(key, value);
            }
        }

        void ICollection.CopyTo(Array array, int index) { throw new NotSupportedException(); }

        public int Count
        {
            get { return _Res.Count + _MergedDictionaries.Count; }
        }

        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot { get { return null; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Res.GetEnumerator();
        }

        public object FindName(string name)
        {
            return _NameScope.FindName(name);
        }

        public void RegisterName(string name, object scopedElement)
        {
            _NameScope.RegisterName(name, scopedElement);
        }

        public void UnregisterName(string name)
        {
            _NameScope.UnregisterName(name);
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {

        }
    }
}
