using Godot;
using WhoAmI.Services.RestService;
using WhoAmI.Services.RestService.Providers;

namespace WhoAmI.Interactive.Scripts.Shared.Extensions
{
    public static class NodeExtensions
    {
        public static IHttpService<T> GetHttpService<T>(this Node node)
        {
            HttpService<T> http = new HttpService<T>();
            node.AddChild(http);
            return http;
        }
    }
}
