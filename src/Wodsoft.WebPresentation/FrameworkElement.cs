using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Wodsoft.Web.Data;

namespace Wodsoft.Web
{
    [RuntimeNameProperty("Name")]
    public class FrameworkElement : UIElement, IHaveResources
    {
        private ResourceDictionary _Resources;
        private Style _StyleCache;
        private FrameworkTemplate _Template;

        public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), typeof(FrameworkElement));
        public object DataContext { get { return GetValue(DataContextProperty); } set { SetValue(DataContextProperty, value); } }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(FrameworkElement));
        public string Name { get { return (string)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }

        public static readonly DependencyProperty StyleProperty = DependencyProperty.Register("Style", typeof(Style), typeof(FrameworkElement), new PropertyMetadata(OnStyleChanged));
        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element._StyleCache = e.NewValue as Style;
        }
        public Style Style { get { return (Style)GetValue(StyleProperty); } set { SetValue(StyleProperty, value); } }

        public static readonly DependencyProperty TooltipProperty = DependencyProperty.Register("Tooltip", typeof(string), typeof(FrameworkElement));
        public string Tooltip { get { return (string)GetValue(TooltipProperty); } set { SetValue(TooltipProperty, value); } }

        public static readonly DependencyProperty MarginProperty = DependencyProperty.Register("Margin", typeof(Thickness), typeof(FrameworkElement));
        public Thickness Margin { get { return (Thickness)GetValue(MarginProperty); }set { SetValue(MarginProperty, value); } }

        [Ambient]
        public ResourceDictionary Resources
        {
            get
            {
                if (_Resources == null)
                    _Resources = new ResourceDictionary();
                return _Resources;
            }
            set
            {
                _Resources = value;
            }
        }

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

        #region Dependency

        protected override object GetValueCore(DependencyProperty dp)
        {
            if (!HasValue(dp) && _StyleCache != null)
            {
                Setter setter = _StyleCache.Setters.FirstOrDefault(t => t.Property == dp);
                if (setter != null)
                    return setter.Value;
            }
            return base.GetValueCore(dp);
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

        internal FrameworkElement _TemplateChild;
        internal FrameworkElement _TemplatedParent;

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
            if (_StyleCache == null)
            {
                UIElement element = this;
                while (element != null)
                {
                    IHaveResources resourceContainer = element as IHaveResources;
                    if (resourceContainer != null && resourceContainer.Resources.Count > 0)
                    {
                        _StyleCache = resourceContainer.Resources[this.GetType()] as Style;
                        if (_StyleCache != null)
                            break;
                    }
                    element = element.Parent ?? (element as FrameworkElement)?.TemplatedParent;
                }
            }
            if (ElementTemplate == null || ElementTemplate == _Template)
                return;
            ElementTemplate.Seal();
            ElementTemplate.ApplyTemplate(this);
        }

        protected virtual void OnApplyTemplate() { }

        #endregion

    }
}
