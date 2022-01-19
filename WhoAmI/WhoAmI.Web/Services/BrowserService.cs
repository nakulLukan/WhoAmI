using Microsoft.JSInterop;
using WhoAmI.Web.Interfaces;

namespace WhoAmI.Web.Services;

public class BrowserService : IBrowserService
{
    private readonly IJSRuntime jsRuntime;

    public BrowserService(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Get document body scroll top level
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetBodyScrollTop()
    {
        return await jsRuntime.InvokeAsync<int>("getDocumentBodyScrollTop");
    }

    /// <summary>
    /// Get document element scroll top level
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetElementScrollTop()
    {
        return await jsRuntime.InvokeAsync<int>("getDocumentElementScrollTop");
    }
}
