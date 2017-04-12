using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Html
{
    public class HtmlStyleCollection : IDictionary<string, string>
    {
        private Dictionary<string, string> _dict;

        public HtmlStyleCollection()
        {
            _dict = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get
            {
                string value;
                if (_dict.TryGetValue(key, out value))
                    return value;
                return value;
            }

            set
            {
                if (_dict.ContainsKey(key))
                    _dict[key] = value;
                else
                    _dict.Add(key, value);
            }
        }

        public int Count
        {
            get
            {
                return _dict.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return _dict.Keys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                return _dict.Values;
            }
        }

        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
        {
            ((IDictionary<string, string>)_dict).Add(item);
        }

        public void Add(string key, string value)
        {
            _dict.Add(key, value);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)_dict).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)_dict).CopyTo(array, arrayIndex);
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)_dict).GetEnumerator();
        }

        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)_dict).Remove(item);
        }

        public bool Remove(string key)
        {
            return _dict.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
    }
}
