using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wodsoft.Web
{
    public class RenderContext
    {
        public RenderContext(TextWriter writer)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = new UTF8Encoding(false);
            setting.NewLineChars = "\r\n";
            setting.NewLineHandling = NewLineHandling.Replace;
            setting.Indent = true;
            setting.OmitXmlDeclaration = true;
            _Writer = XmlWriter.Create(writer, setting);
            Javascript = new StringWriter();
        }

        private XmlWriter _Writer;
        public XmlWriter Writer
        {
            get
            {
                return _Writer;
            }
        }

        public TextWriter Javascript { get; private set; }
    }
}
