using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public string Path { get; private set; }

        public Task RenderAsync(ViewContext context)
        {
            Application.Current.Properties["Context"] = context;
            _Element.DataContext = context.ViewData.Model;
            _Element.Render(context.Writer);
            return Task.FromResult(0);
        }
    }
}
