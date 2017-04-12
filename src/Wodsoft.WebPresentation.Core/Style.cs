using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Wodsoft.Web
{
    [ContentProperty("Setters"), DictionaryKeyProperty("TargetType")]
    public class Style : ISealable, IHaveResources
    {
        private ResourceDictionary _Resources;
        private SetterCollection _Setters;
        private bool _IsSealed;

        public Style()
        {
            _Setters = new SetterCollection();
        }

        [Ambient]
        public Type TargetType { get; set; }

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

        public SetterCollection Setters { get { return _Setters; } }

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
            Setters.Seal();
        }

        private void CheckSealed()
        {
            if (_IsSealed)
                throw new InvalidOperationException("Can not change values when object was sealed.");
        }

        #endregion
    }
}
