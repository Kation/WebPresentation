using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wodsoft.Web
{
    public interface ISealable
    {
        bool CanSeal { get; }
        bool IsSealed { get; }
        void Seal();
    }
}
