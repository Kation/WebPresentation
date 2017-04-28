using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.ComponentModel;

namespace Wodsoft.Web
{
    public class Setter : ISealable, ISupportInitialize
    {
        private bool _IsSealed;
        private DependencyProperty _Property;
        private object _Value;

        public Setter()
        {
            _IsSealed = false;
        }

        [Ambient]
        public DependencyProperty Property
        {
            get
            {
                return _Property;
            }
            set
            {
                CheckSealed();
                _Property = value;
            }
        }

        [DependsOn("Property")]
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                CheckSealed();
                _Value = value;
            }
        }
        
        public void BeginInit()
        {

        }

        public void EndInit()
        {

        }

        #region Sealable

        bool ISealable.CanSeal
        {
            get { return true; }
        }

        bool ISealable.IsSealed
        {
            get { return _IsSealed; }
        }

        public void Seal()
        {
            if (_IsSealed)
                return;
            _IsSealed = true;
        }

        private void CheckSealed()
        {
            if (_IsSealed)
                throw new InvalidOperationException("Can not change values when object was sealed.");
        }

        #endregion
    }
}
