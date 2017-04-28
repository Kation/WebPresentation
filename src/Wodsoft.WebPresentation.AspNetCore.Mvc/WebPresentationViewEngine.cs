using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Wodsoft.Web.Xaml;

namespace Wodsoft.Web.AspNetCore.Mvc
{
    public class WebPresentationViewEngine : IViewEngine
    {
        private Dictionary<string, WebPresentationView> _Views;

        public WebPresentationViewEngine()
        {
            _Views = new Dictionary<string, WebPresentationView>();
        }

        private IFileInfo GetViewPath(ActionContext context, string controller, string view, string area)
        {
            var env = context.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();
            string path = "";
            if (area != null)
                path = area + "/";
            path += "views";
            path += "/" + controller + "/" + view + ".xaml";
            var file = env.ContentRootFileProvider.GetFileInfo(path);
            if (file.Exists)
                return file;
            return null;
        }
        
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            IFileInfo file = GetViewPath(context, context.RouteData.Values["controller"] as string, viewName, context.RouteData.Values["area"] as string);
            if (file == null)
                return null;

            if (_Views.ContainsKey(file.PhysicalPath))
                return ViewEngineResult.Found("WebPresentation", _Views[file.PhysicalPath]);
            WebPresentationView view;
            using (Stream stream = file.CreateReadStream())
            {
                XamlReader reader = new XamlReader();
                FrameworkElement element = reader.Load(stream) as FrameworkElement;
                if (element == null)
                    return ViewEngineResult.NotFound(viewName, new string[0]);
                view = new WebPresentationView((Application)context.HttpContext.Items["WebPresentation_Root"], element);
                _Views.Add(file.PhysicalPath, view);
            }
            if (view == null)
                return null;
            return ViewEngineResult.Found("WebPresentation", view);
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return ViewEngineResult.NotFound(viewPath, new string[0]);
        }
    }
}
