using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wodsoft.Web.AspNetCore.Mvc
{
    public class WebPresentationView : IView
    {
        private FrameworkElement _Element;

        public WebPresentationView(Application application, FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            _Element = element;
            application.Root = element;
        }

        public string Path { get; private set; }

        public Task RenderAsync(ViewContext context)
        {
            //Application.Current.Properties["Context"] = context;
            _Element.DataContext = context.ViewData.Model;
            _Element.Render(context.Writer);
            return Task.FromResult(0);
        }
    }
}
