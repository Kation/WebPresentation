using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Windows.Markup;

namespace Wodsoft.Web
{
    [ContentProperty("Template")]
    public abstract class FrameworkTemplate : INameScope, ISealable, IQueryAmbient
    {
        private NameScope _NameScope;
        private bool _IsSealed;
        private bool _HasXamlNodeContent;
        private ResourceDictionary _Resources;
        private TemplateContent _TemplateContent;

        public FrameworkTemplate()
        {
            _NameScope = new NameScope();
        }

        [DefaultValue(null), Ambient]
        public TemplateContent Template
        {
            get
            {
                return _TemplateContent;
            }
            set
            {
                CheckSealed();
                if (_HasXamlNodeContent)
                    throw new XamlParseException("Template content set twice");
                _TemplateContent = value;
                _HasXamlNodeContent = true;
            }
        }

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
                CheckSealed();
                _Resources = value;
            }
        }

        protected virtual void ValidateTemplatedParent(FrameworkElement templatedParent) { }

        internal void ApplyTemplate(FrameworkElement element)
        {
            FrameworkElement child = Template.LoadContent() as FrameworkElement;
            if (child == null)
                return;
            child._TemplatedParent = element;
            element._TemplateChild = child;
            element.InternalAddVisualChild(child);
        }

        #region NameScope

        public object FindName(string name)
        {
            return _NameScope.FindName(name);
        }

        public void RegisterName(string name, object scopedElement)
        {
            _NameScope.RegisterName(name, scopedElement);
        }

        public void UnregisterName(string name)
        {
            _NameScope.UnregisterName(name);
        }

        #endregion

        #region Sealable

        bool ISealable.CanSeal { get { return true; } }

        bool ISealable.IsSealed
        {
            get { return _IsSealed; }
        }

        public void Seal()
        {
            if (_IsSealed)
                return;

            //验证Template完整性

            _IsSealed = true;
        }

        protected void CheckSealed()
        {
            if (_IsSealed)
                throw new InvalidOperationException("Can not chang after sealed.");
        }

        #endregion

        #region QueryAmbient

        bool IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
        {
            return (!(propertyName == "Resources") || this._Resources != null) && (!(propertyName == "Template") || this._HasXamlNodeContent);
        }

        #endregion
    }
}
