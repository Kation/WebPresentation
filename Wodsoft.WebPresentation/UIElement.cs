using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class UIElement : Visual
    {
        private Dictionary<DependencyProperty, object> _CacheValue;

        public UIElement()
        {
            _CacheValue = new Dictionary<DependencyProperty, object>();
        }

        protected override object GetValueCore(DependencyProperty dp)
        {
            if (_IsRendering)
            {
                if (_CacheValue.ContainsKey(dp))
                    return _CacheValue[dp];
                var value = base.GetValueCore(dp);
                _CacheValue.Add(dp, value);
                return value;
            }
            return base.GetValueCore(dp);
        }

        [ThreadStatic]
        private static bool _IsRendering;
        
        public sealed override void Render(TextWriter writer)
        {
            //_IsRendering = true;
            _CacheValue.Clear();
            try
            {
                RenderContext context = new RenderContext(writer);
                context.Writer.WriteStartDocument();
                OnRender(context);
                context.Writer.WriteEndDocument();
                context.Writer.Flush();
            }
            finally
            {
                _IsRendering = false;
            }
        }

        public virtual void OnRender(RenderContext context)
        {

        }
    }
}
