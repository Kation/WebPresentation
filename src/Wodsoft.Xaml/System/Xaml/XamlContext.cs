using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Markup;
namespace System.Xaml
{
    public class XamlContext
    {
        private Dictionary<string, string> _Namespaces;
        private Dictionary<string, Dictionary<string, XamlType>> _XamlTypes;
        private Dictionary<XamlType, Dictionary<string, XamlMember>> _XamlMembers;
        private Dictionary<string, Dictionary<string, Type>> _NamespaceTypes;

        public XamlContext(XamlSchemaContext schemaContext)
        {
            SchemaContext = schemaContext;
            _Namespaces = new Dictionary<string, string>();
            _XamlTypes = new Dictionary<string, Dictionary<string, XamlType>>();
            _XamlMembers = new Dictionary<XamlType, Dictionary<string, XamlMember>>();
        }

        public XamlSchemaContext SchemaContext { get; private set; }

        public void LoadNamespaces()
        {
            var source = SchemaContext.ReferenceAssemblies.Select(t =>
            {
                return t.GetCustomAttributes<XmlnsDefinitionAttribute>().GroupBy(g => g.XmlNamespace)
                .ToDictionary(d => d.Key, d => d.SelectMany(s => t.GetTypes().Where(x => x.GetTypeInfo().IsPublic && x.Namespace == s.ClrNamespace)));
            }).Where(t => t.Count > 0).ToArray();
            var keys = source.SelectMany(t => t.Keys).Distinct();
            _NamespaceTypes = keys.ToDictionary(d => d, d => source.Select(t =>
             {
                 IEnumerable<Type> types;
                 if (!t.TryGetValue(d, out types))
                     types = null;
                 return types;
             }).Where(t => t != null).SelectMany(t => t).ToDictionary(t => t.Name, t => t));
            //}).Where(t => t != null).SelectMany(t => t).GroupBy(t => t.Name).Select(t => t.First()).ToDictionary(t => t.Name, t => t));
        }

        public void AddNamespacePrefix(string prefix, string xamlNamespace)
        {
            _Namespaces.Add(prefix, xamlNamespace);
        }

        public string FindNamespaceByPrefix(string prefix)
        {
            string ns;
            _Namespaces.TryGetValue(prefix, out ns);
            return ns;
        }

        public XamlType FindXamlType(string ns, string typeName)
        {
            Dictionary<string, XamlType> types;
            if (!_XamlTypes.TryGetValue(ns, out types))
            {
                types = new Dictionary<string, Xaml.XamlType>();
                _XamlTypes.Add(ns, types);
            }
            XamlType type;
            if (!types.TryGetValue(typeName, out type))
            {
                if (_NamespaceTypes.ContainsKey(ns))
                {
                    var nTypes = _NamespaceTypes[ns];
                    Type uType;
                    if (nTypes.TryGetValue(typeName, out uType))
                        type = new XamlType(uType, SchemaContext);
                    else
                        type = new XamlType(ns, typeName, new List<XamlType>(), SchemaContext);
                }
                else
                    type = new XamlType(ns, typeName, new List<XamlType>(), SchemaContext);
                types.Add(typeName, type);
            }
            return type;
        }

        public XamlMember FindXamlMember(XamlType type, string memberName, bool isAttached)
        {
            Dictionary<string, XamlMember> members;
            if (!_XamlMembers.TryGetValue(type, out members))
            {
                members = new Dictionary<string, Xaml.XamlMember>();
                _XamlMembers.Add(type, members);
            }
            XamlMember member;
            if (!members.TryGetValue(memberName, out member))
            {
                bool success = false;
                if (type.UnderlyingType != null)
                {
                    if (isAttached)
                    {
                        MethodInfo getter, setter;
                        getter = type.UnderlyingType.GetMethod("Get" + memberName);
                        setter = type.UnderlyingType.GetMethod("Set" + memberName);
                        if (getter != null || setter != null)
                        {
                            member = new XamlMember(memberName, getter, setter, SchemaContext);
                            success = true;
                        }
                    }
                    else
                    {
                        PropertyInfo property = type.UnderlyingType.GetProperty(memberName);
                        if (property != null)
                        {
                            member = new XamlMember(property, SchemaContext);
                            success = true;
                        }
                    }
                }
                if (!success)
                    member = new XamlMember(memberName, type, isAttached);
                members.Add(memberName, member);
            }
            return member;
        }
    }
}
