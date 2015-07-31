using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class DependencyObject
    {
        private Dictionary<DependencyProperty, object> _LocalValue;

        public DependencyObject()
        {
            _LocalValue = new Dictionary<DependencyProperty, object>();
        }

        public void ClearValue(DependencyProperty dp)
        {
            if (dp == null)
                throw new ArgumentNullException("dp");
            if (dp.ReadOnly)
                throw new InvalidOperationException("Readonly property not allowed to change.");
            ClearValueCore(dp);
        }

        public void ClearValue(DependencyPropertyKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            ClearValueCore(key.Property);
        }

        protected virtual void ClearValueCore(DependencyProperty dp)
        {
            if (_LocalValue.ContainsKey(dp))
            {
                object oldValue = _LocalValue[dp];
                _LocalValue.Remove(dp);
                if (oldValue != dp.DefaultMetadata.DefaultValue && dp.DefaultMetadata.PropertyChangedCallback != null)
                {
                    DependencyPropertyChangedEventArgs e = new DependencyPropertyChangedEventArgs(dp, oldValue, dp.DefaultMetadata.DefaultValue);
                    dp.DefaultMetadata.PropertyChangedCallback(this, e);
                }
            }
        }

        public object GetValue(DependencyProperty dp)
        {
            if (dp == null)
                throw new ArgumentNullException("dp");
            return GetValueCore(dp);
        }

        protected virtual object GetValueCore(DependencyProperty dp)
        {
            if (_LocalValue.ContainsKey(dp))
            {
                object value = _LocalValue[dp];
                Expression expression = value as Expression;
                if (dp.PropertyType != typeof(Expression) && expression != null)
                    value = expression.GetValue(this, dp);
                return value;
            }
            return null;
        }

        public void SetValue(DependencyProperty dp, object value)
        {
            if (dp == null)
                throw new ArgumentNullException("dp");
            if (dp.ReadOnly)
                throw new InvalidOperationException("Readonly property not allowed to change.");
            SetValueCore(dp, value);
        }

        public void SetValue(DependencyPropertyKey key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            SetValueCore(key.Property, value);
        }

        protected virtual void SetValueCore(DependencyProperty dp, object value)
        {
            if (dp.ValidateValueCallback != null)
                if (!dp.ValidateValueCallback(value))
                    throw new ArgumentException("Value invalid.");
            if (dp.DefaultMetadata.CoerceValueCallback != null)
                value = dp.DefaultMetadata.CoerceValueCallback(this, value);
            object oldValue;
            if (_LocalValue.ContainsKey(dp))
            {
                oldValue = _LocalValue[dp];
                _LocalValue[dp] = value;
            }
            else
            {
                oldValue = dp.DefaultMetadata.DefaultValue;
                _LocalValue.Add(dp, value);
            }
            if (dp.DefaultMetadata.PropertyChangedCallback != null)
            {
                DependencyPropertyChangedEventArgs e = new DependencyPropertyChangedEventArgs(dp, oldValue, value);
                dp.DefaultMetadata.PropertyChangedCallback(this, e);
            }
        }
    }
}
