using Microsoft.AspNetCore.Components;
using Wodsoft.WebPresentation.Internals;

namespace Wodsoft.WebPresentation
{
    public class WebPresentationComponent : IComponent
    {
        public void Attach(RenderHandle renderHandle)
        {
            var width = Emscripten.EM_ASM_INT("screen.availWidth");
            Console.WriteLine(width);
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            return Task.CompletedTask;
        }
    }
}
