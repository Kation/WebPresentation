using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Wodsoft.Web.Data;

namespace Wodsoft.Web
{
    [RuntimeNameProperty("Name")]
    public class FrameworkElement : UIElement
    {
        internal FrameworkElement _TemplateChild;
        internal FrameworkElement _TemplatedParent;
        
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), typeof(FrameworkElement));
        public object DataContext { get { return GetValue(DataContextProperty); } set { SetValue(DataContextProperty, value); } }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(FrameworkElement));
        public string Name { get { return (string)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }

        public static readonly DependencyProperty StyleProperty = DependencyProperty.Register("Style", typeof(Style), typeof(FrameworkElement));
        public Style Style { get { return (Style)GetValue(StyleProperty); } set { SetValue(StyleProperty, value); } }

        #region Binding

        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            if (dp == null)
                throw new ArgumentNullException("dp");
            if (dp.ReadOnly)
                throw new InvalidOperationException("Readonly property not allowed to change.");
            return SetBindingCore(dp, binding);
        }

        public BindingExpressionBase SetBinding(DependencyPropertyKey key, BindingBase binding)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return SetBindingCore(key.Property, binding);
        }

        private BindingExpressionBase SetBindingCore(DependencyProperty dp, BindingBase binding)
        {
            base.ClearValueCore(dp);
            BindingExpressionBase expression = binding.GetExpression(this, dp, null);
            SetValueCore(dp, expression);
            return expression;
        }

        internal void SetBindingCore(DependencyProperty dp, BindingExpressionBase expression)
        {
            SetValueCore(dp, expression);
        }

        #endregion

        #region Render

        public override void OnRender(RenderContext context)
        {
            ApplyTemplate();
            if (_TemplateChild != null)
                _TemplateChild.OnRender(context);
        }

        #endregion

        #region Template

        protected override int VisualChildrenCount
        {
            get
            {
                return _TemplateChild == null ? 0 : 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (_TemplateChild == null || index != 0)
                throw new ArgumentOutOfRangeException("index");
            return _TemplateChild;
        }

        protected virtual FrameworkTemplate ElementTemplate { get { return null; } }

        protected FrameworkElement TemplatedParent { get { return _TemplatedParent; } }

        public void ApplyTemplate()
        {
            if (ElementTemplate == null)
                return;
            ElementTemplate.ApplyTemplate(this);
        }

        protected virtual void OnApplyTemplate() { }

        #endregion
    }
}
