using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Internals
{
    internal class Emscripten
    {
        [DllImport("libEmscriptenWrapper", EntryPoint = "run_script_int")]
        public static extern int EM_ASM_INT(string code);

        [DllImport("libEmscriptenWrapper", EntryPoint = "run_script_string")]
        public static extern string EM_ASM_STRING(string code);
    }
}
