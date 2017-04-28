using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Markup;

namespace Wodsoft.Web.Controls
{
    [DictionaryKeyProperty("TargetType")]
    public class ControlTemplate : FrameworkTemplate
    {
        private Type _TargetType;
        [DefaultValue(null), Ambient]
        public Type TargetType
        {
            get { return _TargetType; }
            set
            {
                CheckSealed();
                _TargetType = value;
            }
        }

        protected override void ValidateTemplatedParent(FrameworkElement templatedParent)
        {
            if (templatedParent == null)
                throw new ArgumentNullException("templatedParent");
            if (_TargetType != null && !_TargetType.GetTypeInfo().IsInstanceOfType(templatedParent))
                throw new ArgumentException("Template target type mismatch.");
        }
    }
}
