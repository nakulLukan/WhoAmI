using Godot;
using System;

public class PlayerLookAt : Spatial
{
    [Export] public Spatial TargetPositionChildObject {get;set;}
    [Export] public float CamFollowSpeed { get; set; } = 1F;

    Spatial player = null;
    Vector3 targetDistanceFromPlayer;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetTree().Root.GetNode<Spatial>(NodePath.Player);
        TargetPositionChildObject = player.GetChild<Spatial>(3);
        targetDistanceFromPlayer = player.Transform.origin - TargetPositionChildObject.Transform.origin;
        InMemoryStore.AddGameObject(TagConstant.MainCamera, this);
    }

    public override void _Process(float delta)
    {
        this.SafeLookAt(player.GlobalTransform.origin, Vector3.Up);
        GlobalTranslation = GlobalTranslation.LinearInterpolate(TargetPositionChildObject.GlobalTranslation, delta * CamFollowSpeed);
    }
}
