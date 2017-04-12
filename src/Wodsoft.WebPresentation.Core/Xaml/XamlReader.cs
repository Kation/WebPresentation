using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;
using System.Xml;

namespace Wodsoft.Web.Xaml
{
    public class XamlReader
    {
        public object Load(Stream stream)
        {
            return Load(new XamlXmlReader(stream, GetSchemaContext()));
        }

        public object Load(XmlReader reader)
        {
            return Load(new XamlXmlReader(reader, GetSchemaContext()));
        }

        public object Load(TextReader reader)
        {
            return Load(new XamlXmlReader(reader, GetSchemaContext()));
        }

        public object Load(string filename)
        {
            return Load(new XamlXmlReader(filename, GetSchemaContext()));
        }

        private XamlSchemaContext GetSchemaContext()
        {
            //List<Assembly> assemblies = new List<Assembly>
            //{
            //    Assembly.Load("Wodsoft.WebPresentation.Core"),
            //    Assembly.Load("Wodsoft.WebPresentation")
            //};
            //AppDomain.CurrentDomain.SetupInformation.PrivateBinPath
            XamlSchemaContext schemaContext = new XamlSchemaContext(AppDomain.CurrentDomain.GetAssemblies());
            return schemaContext;
        }

        private object Load(XamlXmlReader reader)
        {
            XamlObjectWriter writer = new ObjectWriter();
            while (reader.Read())
            {
                writer.WriteNode(reader);
            }
            writer.Close();
            reader.Close();
            if (writer.Result is DependencyObject && !(writer is INameScope))
                ((DependencyObject)writer.Result).SetValue(NameScope.NameScopeProperty, writer.RootNameScope);
            return writer.Result;
        }
    }
}
