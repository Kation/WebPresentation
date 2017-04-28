using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Windows.Markup;

namespace Wodsoft.Web
{
    [XamlDeferLoad(typeof(TemplateContentLoader), typeof(FrameworkElement))]
    public class TemplateContent
    {
        private MemoryStream _Cache;
        private XamlSchemaContext _SchemaContext;

        public TemplateContent(XamlReader xamlReader)
        {
            if (xamlReader == null)
                throw new ArgumentNullException("xamlReader");
            _SchemaContext = xamlReader.SchemaContext;
            _Cache = new MemoryStream();
            //xamlReader = xamlReader.ReadSubtree();
            XamlXmlWriter writer = new XamlXmlWriter(_Cache, xamlReader.SchemaContext);
            while (xamlReader.Read())
            {
                writer.WriteNode(xamlReader);
            }
            writer.Close();
        }

        public object LoadContent()
        {
            _Cache.Position = 0;
            XamlReader reader = new XamlXmlReader(_Cache, _SchemaContext);
            XamlObjectWriter writer = new Wodsoft.Web.Xaml.ObjectWriter(reader.SchemaContext);
            while (reader.Read())
            {
                writer.WriteNode(reader);
            }
            writer.Close();
            reader.Close();
            return writer.Result;
        }
    }
}
