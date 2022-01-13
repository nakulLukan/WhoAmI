using Microsoft.JSInterop;

namespace WhoAmI.Web.Services
{
    public class SimpleScene
    {
        public IJSRuntime js { get; set; }

        public SimpleScene(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task RenderScene(string elementId)
        {
            await js.InvokeVoidAsync("printConsole");
            var jsRef = await js.InvokeAsync<IJSObjectReference>("local.getThree");
            var temp = await jsRef.InvokeAsync<string>("getName");
        }
    }
}
