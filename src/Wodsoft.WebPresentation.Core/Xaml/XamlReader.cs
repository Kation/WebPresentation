using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyModel;
using System.Xaml;
using System.Windows.Markup;
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

        private static Assembly[] _Assemblies;

        private XamlSchemaContext GetSchemaContext()
        {
            if (_Assemblies == null)
            {
                _Assemblies = DependencyContext.Default.RuntimeLibraries.Select(s => s.Name)
                    .Select(t =>
                    {
                        try
                        {
                            return Assembly.Load(new AssemblyName(t));
                        }
                        catch (Exception)
                        {

                            return null;
                        }
                    }).Where(t => t != null).ToArray();
            }
            XamlSchemaContext schemaContext = new XamlSchemaContext(_Assemblies);
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
