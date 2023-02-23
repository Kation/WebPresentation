using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wodsoft.Web.Internals
{
    internal class WebGL
    {
        [DllImport("libWebGLWrapper", EntryPoint = "webgl_create_context")]
        public static extern WebGLContext CreateContext(string target, ref WebGLContextAttributes attributes);

        [DllImport("libWebGLWrapper", EntryPoint = "webgl_init_context_attributes")]
        public static extern void InitContextAttributes(ref WebGLContextAttributes attributes);

        [DllImport("libWebGLWrapper", EntryPoint = "webgl_make_context_current")]
        public static extern EmscriptenResult SetContext(WebGLContext context);

        [DllImport("libWebGLWrapper", EntryPoint = "webgl_clearColor")]
        public static extern void ClearColor(float red, float green, float blue, float alpha);

        [DllImport("libWebGLWrapper", EntryPoint = "webgl_clear")]
        public static extern void Clear(uint mask = GL_COLOR_BUFFER_BIT);

        public const uint GL_COLOR_BUFFER_BIT = 0x00004000;
    }

    internal struct WebGLContextAttributes
    {
        public WebGLContextAttributes()
        {
        }

        public bool Alpha = true;
        public bool Depth = true;
        public bool Stencil = false;
        public bool Antialias = true;
        public bool PremultipliedAlpha = true;
        public bool PreserveDrawingBuffer = false;
        public WebGLPowerPreference PowerPreference = WebGLPowerPreference.DEFAULT;
        public bool FailIfMajorPerformanceCaveat = false;
        public int MajorVersion = 1;
        public int MinorVersion = 0;
        public bool EnableExtensionsByDefault = true;
        public bool ExplicitSwapControl = false;
        public WebGLContextProxyMode ProxyContextToMainThread = WebGLContextProxyMode.DISALLOW;
        public bool RenderViaOffscreenBackBuffer = false;

    }

    internal enum WebGLPowerPreference : int
    {
        DEFAULT = 0,
        LOW_POWER = 1,
        HIGH_PERFORMANCE = 2
    }

    internal enum WebGLContextProxyMode : int
    {
        DISALLOW = 0,
        FALLBACK = 1,
        ALWAYS = 2
    }

    internal struct WebGLContext
    {
        public int Handle;
    }

    internal enum EmscriptenResult
    {
        SUCCESS = 0,
        DEFERRED = 1,
        NOT_SUPPORTED = -1,
        FAILED_NOT_DEFERRED = -2,
        INVALID_TARGET = -3,
        UNKNOWN_TARGET = -4,
        INVALID_PARAM = -5,
        FAILED = -6,
        NO_DATA = -7,
        TIMED_OUT = -8
    }
}
