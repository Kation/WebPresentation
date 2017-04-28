using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace System.ComponentModel
{
#if !NET451
    /// <summary>
    /// Interface for objects that support initialization.
    /// </summary>
    public interface ISupportInitialize
	{
		/// <summary>
		/// Called before initialization from serialization.
		/// </summary>
		void BeginInit();

		/// <summary>
		/// Called after initialization from serialization.
		/// </summary>
		void EndInit();
	}
#endif
}