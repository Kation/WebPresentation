using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wodsoft.Web.Data;

namespace Wodsoft.Web.Mvc
{
    public class ViewBagBindingExpression : BindingExpressionBase
    {
        public ViewBagBindingExpression(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            Name = name;
        }

        public string Path { get; set; }

        public string Name { get; private set; }

        public override object GetValueCore(DependencyObject d, DependencyProperty dp)
        {
            if (Application.Current == null)
                throw new NotSupportedException("Web application not found.");
            ViewContext context =  Application.Current.Properties["Context"] as ViewContext;

            object value = context.ViewData[Name];
            if (value == null)
                return null;
            if (Path == null)
                return value;

            string[] split = Path.Split('.');
            for (int i = 0; i < split.Length; i++)
            {
                if (value == null)
                    return null;
                string property = split[i];
                var propertyInfo = value.GetType().GetProperty(property);
                if (propertyInfo == null)
                    return null;
                value = propertyInfo.GetValue(value);
            }
            return value;
        }
    }
}
