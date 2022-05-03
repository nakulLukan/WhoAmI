using Godot;
using System;

public class BoxRotate : CSGBox
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Rotate(Vector3.Up, 2 * delta);
    }

}
