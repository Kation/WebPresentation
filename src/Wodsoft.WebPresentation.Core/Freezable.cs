using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public abstract class Freezable : ISealable
    {
        private bool _IsFrozen;

        public virtual bool CanFreeze { get { return !IsFrozen; } }
        public bool IsFrozen { get { return _IsFrozen; } }
        public virtual void Freeze()
        {
            _IsFrozen = true;
        }
        protected virtual void CheckFrozen()
        {
            if (IsFrozen)
                throw new InvalidOperationException("Object freezed.");
        }

        bool ISealable.CanSeal
        {
            get { return CanFreeze; }
        }

        bool ISealable.IsSealed
        {
            get { return _IsFrozen; }
        }

        void ISealable.Seal()
        {
            Freeze();
        }
    }
}
