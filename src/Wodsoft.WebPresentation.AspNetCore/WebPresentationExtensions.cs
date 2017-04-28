using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wodsoft.Web;
using Wodsoft.Web.Xaml;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebPresentationExtensions
    {
        public static void UseWebPresentation(this IApplicationBuilder app, Func<Application> applicationFactory)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            app.UseMiddleware<WebPresentationMiddleware>(applicationFactory);
        }

        public static void UseWebPresentation(this IApplicationBuilder app, string applicationXaml)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            app.UseMiddleware<WebPresentationMiddleware>(new Func<Application>(() =>
            {
                XamlReader reader = new XamlReader();
                Application application = reader.Load(applicationXaml) as Application;
                if (application == null)
                    throw new ArgumentException("XAML内容不正确。");
                return application;
            }));
        }
    }
}