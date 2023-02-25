using Godot;
using System;

public class GameAction : Area
{
    [Export] public Shape Shape { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetChild<CollisionShape>(0).Shape = Shape;
    }

    public void OnActionAreaEntered(Node body)
    {
        Utility.PrintPretty(body.Name);
    }
}
