using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    [TypeConverter(typeof(DependencyPropertyConverter))]
    public sealed class DependencyProperty
    {
        private static Hashtable _PropertyFromName;

        public static readonly object UnsetValue = new object();

        static DependencyProperty()
        {
            _PropertyFromName = new Hashtable();
        }

        private DependencyProperty()
        {

        }

        public PropertyMetadata DefaultMetadata { get; private set; }
        public string Name { get; private set; }
        public Type OwnerType { get; private set; }
        public Type PropertyType { get; private set; }
        public bool ReadOnly { get; private set; }
        public ValidateValueCallback ValidateValueCallback { get; private set; }

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
        {
            return Register(name, propertyType, ownerType, null, null);
        }
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            return Register(name, propertyType, ownerType, typeMetadata, null);
        }
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
        {
            PropertyMetadata defaultMetadata = null;
            if (typeMetadata != null && typeMetadata.DefaultValue != null)
                defaultMetadata = new PropertyMetadata(typeMetadata.DefaultValue);
            DependencyProperty dp = RegisterCommon(name, propertyType, ownerType, defaultMetadata, validateValueCallback);
            if (typeMetadata != null)
                dp.OverrideMetadata(ownerType, typeMetadata);
            else
                if (propertyType.GetTypeInfo().IsValueType)
                    dp.DefaultMetadata.DefaultValue = Activator.CreateInstance(propertyType);
            dp.ReadOnly = false;
            return dp;
        }

        public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            return RegisterReadOnly(name, propertyType, ownerType, typeMetadata);
        }
        public static DependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
        {
            DependencyProperty dp = Register(name, propertyType, ownerType, typeMetadata, validateValueCallback);
            dp.ReadOnly = true;
            DependencyPropertyKey key = new DependencyPropertyKey(dp);
            return key;
        }

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType)
        {
            return RegisterAttached(name, propertyType, ownerType, null, null);
        }
        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            return RegisterAttached(name, propertyType, ownerType, typeMetadata, null);
        }
        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
        {
            DependencyProperty dp = RegisterCommon(name, propertyType, ownerType, typeMetadata, validateValueCallback);
            if (propertyType.GetTypeInfo().IsValueType && dp.DefaultMetadata.DefaultValue == null)
                dp.DefaultMetadata.DefaultValue = Activator.CreateInstance(propertyType);
            dp.ReadOnly = false;
            return dp;
        }

        public static DependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            return RegisterAttachedReadOnly(name, propertyType, ownerType, typeMetadata, null);
        }
        public static DependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
        {
            DependencyProperty dp = RegisterAttached(name, propertyType, ownerType, typeMetadata, validateValueCallback);
            dp.ReadOnly = true;
            DependencyPropertyKey key = new DependencyPropertyKey(dp);
            return key;
        }

        private static DependencyProperty RegisterCommon(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            if (propertyType == null)
                throw new ArgumentNullException("propertyType");
            if (ownerType == null)
                throw new ArgumentNullException("ownerType");
            FromNameKey key = new FromNameKey(name, ownerType);
            if (_PropertyFromName.Contains(key))
                throw new ArgumentException("The property \"" + name + "\" is exists for \"" + ownerType.FullName + "\"");

            if (typeMetadata == null)
                typeMetadata = new PropertyMetadata();

            DependencyProperty dp = new DependencyProperty();
            dp.Name = name;
            dp.PropertyType = propertyType;
            dp.OwnerType = ownerType;
            dp.DefaultMetadata = typeMetadata;
            dp.ValidateValueCallback = validateValueCallback;

            _PropertyFromName.Add(key, dp);

            return dp;
        }

        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
        {
            if (forType == null)
                throw new ArgumentNullException("forType");
            if (typeMetadata == null)
                throw new ArgumentNullException("typeMetadata");
            if (ReadOnly)
                throw new InvalidOperationException("Readonly property override not allowed.");
            OverrideMetadataCore(forType, typeMetadata);
        }
        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata, DependencyPropertyKey key)
        {
            if (forType == null)
                throw new ArgumentNullException("forType");
            if (typeMetadata == null)
                throw new ArgumentNullException("typeMetadata");
            if (key == null)
                throw new ArgumentNullException("key");
            if (key.Property != this)
                throw new InvalidOperationException("Readonly property key not authorized.");
            OverrideMetadataCore(forType, typeMetadata);
        }
        private void OverrideMetadataCore(Type forType, PropertyMetadata typeMetadata)
        {
            typeMetadata.Apply(this, forType);

            DefaultMetadata.Merge(typeMetadata, this);
        }

        public DependencyProperty AddOwner(Type ownerType)
        {
            return AddOwner(ownerType, null);
        }
        public DependencyProperty AddOwner(Type ownerType, PropertyMetadata typeMetadata)
        {
            if (ownerType == null)
                throw new ArgumentNullException("ownerType");
            DefaultMetadata.Apply(this, PropertyType);
            DependencyProperty dp = RegisterCommon(Name, PropertyType, ownerType, typeMetadata, ValidateValueCallback);
            dp.DefaultMetadata.Merge(DefaultMetadata, dp);
            dp.ReadOnly = ReadOnly;
            return dp;
        }

        internal static DependencyProperty FromName(string name, Type ownerType)
        {
            var fields = ownerType.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (fields.Length > 0) fields[0].GetValue(null);
            FromNameKey key = new FromNameKey(name, ownerType);
            DependencyProperty dp = (DependencyProperty)_PropertyFromName[key];
            return dp;
        }

        public override string ToString()
        {
            return OwnerType.Name + "." + Name;
        }

        private class FromNameKey
        {
            public FromNameKey(string name, Type ownerType)
            {
                _name = name;
                _ownerType = ownerType;

                _hashCode = _name.GetHashCode() ^ _ownerType.GetHashCode();
            }

            public void UpdateNameKey(Type ownerType)
            {
                _ownerType = ownerType;

                _hashCode = _name.GetHashCode() ^ _ownerType.GetHashCode();
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }

            public override bool Equals(object o)
            {
                if ((o != null) && (o is FromNameKey))
                {
                    return Equals((FromNameKey)o);
                }
                else
                {
                    return false;
                }
            }

            public bool Equals(FromNameKey key)
            {
                return (_name.Equals(key._name) && (_ownerType == key._ownerType));
            }

            private string _name;
            private Type _ownerType;

            private int _hashCode;
        }
    }
}
