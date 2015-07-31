using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Controls
{
    public class Image : FrameworkElement
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(Image));
        public Uri Source { get { return (Uri)GetValue(SourceProperty); } set { SetValue(SourceProperty, value); } }
    }
}
