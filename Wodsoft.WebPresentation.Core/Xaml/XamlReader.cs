using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return Load(new XamlXmlReader(stream));
        }

        public object Load(XmlReader reader)
        {
            return Load(new XamlXmlReader(reader));
        }

        public object Load(TextReader reader)
        {
            return Load(new XamlXmlReader(reader));
        }

        public object Load(string filename)
        {
            return Load(new XamlXmlReader(filename));
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
