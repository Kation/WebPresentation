using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class PropertyMetadata : Freezable
    {
        public PropertyMetadata() { }
        public PropertyMetadata(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
        public PropertyMetadata(PropertyChangedCallback propertyChangedCallback)
        {
            PropertyChangedCallback = propertyChangedCallback;
        }
        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
        {
            DefaultValue = defaultValue;
            PropertyChangedCallback = propertyChangedCallback;
        }

        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback)
        {
            DefaultValue = defaultValue;
            PropertyChangedCallback = propertyChangedCallback;
            CoerceValueCallback = coerceValueCallback;
        }

        private object _DefaultValue;
        public object DefaultValue
        {
            get
            {
                return _DefaultValue;
            }
            set
            {
                CheckFrozen();
                _DefaultValue = value;
            }
        }

        //public override void Freeze()
        //{
        //    //var trace = new System.Diagnostics.StackTrace();
        //    //var frame = trace.GetFrame(1);
        //    //var method = frame.GetMethod();
        //    //if (method == null)
        //    //    throw new NotSupportedException();
        //    //if (method.DeclaringType != typeof(PropertyMetadata))
        //    //    throw new NotSupportedException();
        //    //base.Freeze();
        //}

        private PropertyChangedCallback _PropertyChangedCallback;
        public PropertyChangedCallback PropertyChangedCallback
        {
            get
            {
                return _PropertyChangedCallback;
            }
            set
            {
                CheckFrozen();
                _PropertyChangedCallback = value;
            }
        }

        private CoerceValueCallback _CoerceValueCallback;
        public CoerceValueCallback CoerceValueCallback
        {
            get
            {
                return _CoerceValueCallback;
            }
            set
            {
                CheckFrozen();
                _CoerceValueCallback = value;
            }
        }
        
        internal void Merge(PropertyMetadata baseMetadata, DependencyProperty dp)
        {
            CheckFrozen();
            if (_DefaultValue == null)
                _DefaultValue = baseMetadata._DefaultValue;
            if (_PropertyChangedCallback == null)
                _PropertyChangedCallback = baseMetadata._PropertyChangedCallback;
            if (_CoerceValueCallback == null)
                _CoerceValueCallback = baseMetadata._CoerceValueCallback;
            OnMerge(baseMetadata, dp);
        }

        protected virtual void OnMerge(PropertyMetadata baseMetadata, DependencyProperty dp) { }

        internal void Apply(DependencyProperty dp, Type targetType)
        {
            if (IsFrozen)
                return;
            if (_DefaultValue == null)
            {
                if (targetType.GetTypeInfo().IsValueType)
                    _DefaultValue = Activator.CreateInstance(targetType);
            }
            Freeze();
            OnApply(dp, targetType);
        }

        protected virtual void OnApply(DependencyProperty dp, Type targetType) { }
    }
}
