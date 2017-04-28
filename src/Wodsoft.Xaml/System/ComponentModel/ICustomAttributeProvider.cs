using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel
{
    public class TypeAttributeProvider : ICustomAttributeProvider
    {
        readonly Type type;

        public TypeAttributeProvider(Type type)
        {
            this.type = type;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            var attr = type.GetTypeInfo().GetCustomAttributes(inherit).ToArray();
            return attr.Cast<object>().ToArray();
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            var attr = type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
            return attr.Cast<object>().ToArray();
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return type.GetTypeInfo().IsDefined(attributeType, inherit);
        }
    }

    public class MemberAttributeProvider : ICustomAttributeProvider
    {
        readonly MemberInfo info;

        public MemberAttributeProvider(MemberInfo info)
        {
            this.info = info;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            var attr = info.GetCustomAttributes(inherit).ToArray();
            return attr.Cast<object>().ToArray();
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            var attr = info.GetCustomAttributes(attributeType, inherit);
            return attr.Cast<object>().ToArray();
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return info.IsDefined(attributeType, inherit);
        }
    }
}
