using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xaml;

namespace Wodsoft.Web
{
    public class TemplateContentFrame
    {
        public TemplateContentFrame(XamlReader reader)
        {
            NodeType = reader.NodeType;
            switch (NodeType)
            {
                case XamlNodeType.StartObject:
                    Type = reader.Type;
                    break;
                case XamlNodeType.StartMember:
                    Member = reader.Member;
                    break;
                case XamlNodeType.Value:
                    Value = reader.Value;
                    break;
                case XamlNodeType.NamespaceDeclaration:
                    Namespace = reader.Namespace;
                    break;
            }
        }

        public void Write(XamlWriter writer)
        {
            switch (NodeType)
            {
                case XamlNodeType.EndMember:
                    writer.WriteEndMember();
                    break;
                case XamlNodeType.EndObject:
                    writer.WriteEndObject();
                    break;
                case XamlNodeType.GetObject:
                    writer.WriteGetObject();
                    break;
                case XamlNodeType.NamespaceDeclaration:
                    writer.WriteNamespace(Namespace);
                    break;
                case XamlNodeType.StartMember:
                    writer.WriteStartMember(Member);
                    break;
                case XamlNodeType.StartObject:
                    writer.WriteStartObject(Type);
                    break;
                case XamlNodeType.Value:
                    writer.WriteValue(Value);
                    break;
            }
        }

        public NamespaceDeclaration Namespace { get; private set; }
        
        public object Value { get; private set; }

        public XamlMember Member { get; private set; }

        public XamlType Type { get; private set; }

        public XamlNodeType NodeType { get; private set; }
    }
}
