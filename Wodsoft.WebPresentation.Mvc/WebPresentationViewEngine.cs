using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Wodsoft.Web.Mvc
{
    public class WebPresentationViewEngine : IViewEngine
    {
        private Dictionary<string, WebPresentationView> _Views;

        public WebPresentationViewEngine()
        {
            _Views = new Dictionary<string, WebPresentationView>();
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            string path = GetViewPath(controllerContext.HttpContext.Server, controllerContext.RouteData.GetRequiredString("controller"), partialViewName, controllerContext.RouteData.GetRequiredString("area"));
            if (path == null)
                return null;
            WebPresentationView view = GetView(path);
            if (view == null)
                return null;
            return new ViewEngineResult(view, this);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string path = GetViewPath(controllerContext.HttpContext.Server, controllerContext.RouteData.Values["controller"] as string, viewName, controllerContext.RouteData.Values["area"] as string);
            if (path == null)
                return null;
            WebPresentationView view = GetView(path);
            if (view == null)
                return null;
            return new ViewEngineResult(view, this);
        }

        private string GetViewPath(HttpServerUtilityBase server, string controller, string view, string area)
        {
            string path;
            if (area != null)
                path = server.MapPath("~/" + area + "/views");
            else
                path = server.MapPath("~/views");
            path += "\\" + controller + "\\" + view + ".xaml";
            if (File.Exists(path))
                return path;
            return null;
        }

        private WebPresentationView GetView(string path)
        {
            if (_Views.ContainsKey(path))
                return _Views[path];
            using (Stream stream = File.OpenRead(path))
            {
                try
                {
                    Wodsoft.Web.Xaml.XamlReader reader = new Xaml.XamlReader();
                    FrameworkElement element = reader.Load(stream) as FrameworkElement;
                    if (element == null)
                        return null;
                    WebPresentationView view = new WebPresentationView(element);
                    _Views.Add(path, view);
                    return view;
                }
                catch
                {
                    return null;
                }
            }
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {

        }
    }
}
