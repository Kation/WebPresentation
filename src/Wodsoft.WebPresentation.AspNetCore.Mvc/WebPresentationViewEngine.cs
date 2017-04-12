using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Wodsoft.Web.Mvc
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

        private WebPresentationView GetView(IFileInfo file)
        {
            if (_Views.ContainsKey(file.PhysicalPath))
                return _Views[file.PhysicalPath];
            using (Stream stream = file.CreateReadStream())
            {
                try
                {
                    Wodsoft.Web.Xaml.XamlReader reader = new Xaml.XamlReader();
                    FrameworkElement element = reader.Load(stream) as FrameworkElement;
                    if (element == null)
                        return null;
                    WebPresentationView view = new WebPresentationView(element);
                    _Views.Add(file.PhysicalPath, view);
                    return view;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            IFileInfo file = GetViewPath(context, context.RouteData.Values["controller"] as string, viewName, context.RouteData.Values["area"] as string);
            if (file == null)
                return null;
            WebPresentationView view = GetView(file);
            if (view == null)
                return null;
            return ViewEngineResult.Found("WebPresentation", view);
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            throw new NotImplementedException();
        }
    }
}
