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
    public async Task<T> GetBodyScrollTop<T>()
    {
        return await jsRuntime.InvokeAsync<T>("getDocumentBodyScrollTop");
    }

    /// <summary>
    /// Get document element scroll top level
    /// </summary>
    /// <returns></returns>
    public async Task<T> GetElementScrollTop<T>()
    {
        return await jsRuntime.InvokeAsync<T>("getDocumentElementScrollTop");
    }
}
