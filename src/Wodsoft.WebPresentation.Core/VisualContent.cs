using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class VisualContent : Visual
    {
        public VisualContent(object content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));
            Content = content;
        }

        public object Content { get; private set; }

        public override void Render(TextWriter writer) { writer.Write(Content.ToString()); }
    }
}
