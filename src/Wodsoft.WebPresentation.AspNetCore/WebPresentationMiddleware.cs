using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class WebPresentationMiddleware
    {
        private RequestDelegate _Next;
        private Func<Application> _ApplicationFactory;

        public WebPresentationMiddleware(RequestDelegate next, Func<Application> applicationFactory)
        {
            _Next = next;
            _ApplicationFactory = applicationFactory;
        }

        public Task Invoke(HttpContext context)
        {
            var app = _ApplicationFactory();
            context.Items["WebPresentation_Root"] = app;
            return _Next(context);
        }
    }
}
