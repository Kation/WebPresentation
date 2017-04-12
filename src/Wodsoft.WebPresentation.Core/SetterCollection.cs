using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web
{
    public class SetterCollection: Collection<Setter>, ISealable
    {
        private bool _IsSealed;
        public SetterCollection()
        {
            _IsSealed = false;
        }

        protected override void ClearItems()
        {
            CheckSealed();
            base.ClearItems();
        }

        protected override void InsertItem(int index, Setter item)
        {
            CheckSealed();
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            CheckSealed();
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Setter item)
        {
            CheckSealed();
            base.SetItem(index, item);
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
            foreach (var setter in this)
                setter.Seal();
        }

        private void CheckSealed()
        {
            if (_IsSealed)
                throw new InvalidOperationException("Can not change values when object was sealed.");
        }

        #endregion
    }
}
