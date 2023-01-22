using Godot;
using System;

public class FreeLookCameraControl : CheckBox
{
    Camera parent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        parent = GetParent<Camera>();
    }

    public override void _Toggled(bool buttonPressed)
    {
        parent.Current = buttonPressed;
        GetTree().CallGroup("free_look_camera", "IsFreeLook", buttonPressed);
    }

}
