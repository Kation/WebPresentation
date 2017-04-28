using System;
using System.Collections;
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
            if (_CacheValue.ContainsKey(dp))
                return _CacheValue[dp];
            var value = base.GetValueCore(dp);
            _CacheValue.Add(dp, value);
            return value;
        }

        #region Render

        public sealed override void Render(TextWriter writer)
        {
            _CacheValue.Clear();
            RenderContext context = new RenderContext(writer);
            context.Writer.WriteStartDocument();
            OnRender(context);
            //context.Writer.WriteEndDocument();
            context.Writer.Flush();
        }

        public virtual void OnRender(RenderContext context)
        {

        }

        #endregion

        #region LogicalTree

        private UIElement _Parent;
        public UIElement Parent { get { return _Parent; } }

        protected internal void AddLogicalChild(UIElement child)
        {
            child._Parent = this;
        }

        protected internal void RemoveLogicalChild(UIElement child)
        {
            child._Parent = null;
        }

        protected internal virtual IEnumerator<UIElement> LogicalChildren { get { return null; } }

        #endregion
    }
}
