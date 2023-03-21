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

        public static Spatial GetGravityPoint(this Node node)
        {
            return node.GetNode<Spatial>(NodePath.GravityPoint);
        }

        public static KinematicBody GetPlayer(this Node node){
            return node.GetNode<KinematicBody>(NodePath.Player);
        }
        public static Spatial GetSubPlayer(this Node node){
            return node.GetNode<Spatial>(NodePath.SubPlayer);
        }
    }
}
