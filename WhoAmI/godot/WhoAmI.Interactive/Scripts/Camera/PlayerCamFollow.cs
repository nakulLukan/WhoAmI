using Godot;
using System;

public class PlayerCamFollow : SpringArm
{
    public int CamFollowSpeed { get; set; } = 4;
    private KinematicBody player;
    private Vector3 playerToCamDistanceVector;
    public override void _Ready()
    {
        player = GetTree().Root.GetNode("World/player_body") as KinematicBody;
        playerToCamDistanceVector = player.Translation - this.Translation;
    }

    public override void _Process(float delta)
    {
        this.Translation = this.Translation.LinearInterpolate(player.Translation - playerToCamDistanceVector, CamFollowSpeed * delta);
    }
}
