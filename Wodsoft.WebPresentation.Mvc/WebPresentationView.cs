using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Wodsoft.Web.Mvc
{
    public class WebPresentationView : IView
    {
        private FrameworkElement _Element;

        public WebPresentationView(FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            _Element = element;
            Application.Current.Root = element;
        }

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            Application.Current.Properties["Context"] = viewContext;
            _Element.DataContext = viewContext.ViewData.Model;
            _Element.Render(writer);
        }
    }
}
