using Godot;
using WhoAmI.Services.RestService;
using WhoAmI.Services.RestService.Providers;

public static class NodeExtensions
{
    public static IHttpService<T> GetHttpService<T>(this Node node)
    {
        HttpService<T> http = new HttpService<T>();
        node.AddChild(http);
        return http;
    }

    public static Spatial GetGravityPoint(this Node node) => node.GetNode<Spatial>(NodePath.GravityPoint);

    public static KinematicBody GetPlayer(this Node node) => node.GetNode<KinematicBody>(NodePath.Player);
    public static Spatial GetSubPlayer(this Node node) => node.GetNode<Spatial>(NodePath.SubPlayer);

    public static AudioStreamProvider GetAudioStreamProvider(this Node node) => 
        node.GetNode<AudioStreamProvider>("/root/AudioStreamProvider");

    public static SoundEffectStreamProvider GetSoundEffectStreamProvider(this Node node)
        => node.GetNode<SoundEffectStreamProvider>("/root/SoundEffectStreamProvider");
}
