using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Wodsoft.Web
{
    public class TemplateContentLoader : XamlDeferringLoader
    {
        public override object Load(XamlReader xamlReader, IServiceProvider serviceProvider)
        {
            return new TemplateContent(xamlReader);
        }

        public override XamlReader Save(object value, IServiceProvider serviceProvider)
        {
            throw new NotSupportedException("Not support save.");
        }
    }
}
