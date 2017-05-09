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
        private IXamlObjectWriterFactory _Factory;
        private List<TemplateContentFrame> _Frames;

        public TemplateContent(XamlReader xamlReader, IXamlObjectWriterFactory factory)
        {
            if (xamlReader == null)
                throw new ArgumentNullException("xamlReader");
            _Factory = factory;
            _SchemaContext = xamlReader.SchemaContext;
            _Cache = new MemoryStream();
            _Frames = new List<Web.TemplateContentFrame>();
            //xamlReader = xamlReader.ReadSubtree();
            while (xamlReader.Read())
            {
                _Frames.Add(new Web.TemplateContentFrame(xamlReader));
            }
            //_Reader = xamlReader;//.ReadSubtree();
        }

        public object LoadContent()
        {
            _Cache.Position = 0;
            //XamlReader reader = _Reader; //new XamlXmlReader(_Cache, _SchemaContext);
            var setting = _Factory.GetParentSettings();
            XamlObjectWriter writer = _Factory.GetXamlObjectWriter(setting);
            foreach (var frame in _Frames)
            {
                frame.Write(writer);
            }
            //writer.Close();
            //reader.Close();
            return writer.Result;
        }
    }
}
