using Godot;
using System;

public class FreeLookCamera : Spatial
{
    [Export] public float LookAroundSpeed { get; set; } = 0.004f;
    [Export] public float MouseTranslateSpeed { get; set; } = 5f;
    [Export] public float MousePanSpeed { get; set; } = 0.3f;

    bool middlePressed = false;
    bool rightButtonPressed = false;
    float rotationX;
    float rotationY;

    float rotateAroundX;
    float rotateAroundY;

    CheckBox checkBox;

    public override void _Ready()
    {
        checkBox = GetChild(0).GetChild<CheckBox>(0);
        rotationX = Rotation.x;
        rotationY = Rotation.y;
    }
    
    public override void _Input(InputEvent @event)
    {
        if (!checkBox.Pressed)
        {
            return;
        }

        if (@event is InputEventMouseButton mouseEvent)
        {
            if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Middle)
            {
                middlePressed = mouseEvent.Pressed;
            }
            else if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.WheelUp)
            {
                Translation += (Transform.basis.z * -MouseTranslateSpeed);
            }
            else if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.WheelDown)
            {
                Translation += (Transform.basis.z * MouseTranslateSpeed);
            }
            else if ((ButtonList)mouseEvent.ButtonIndex == ButtonList.Right)
            {
                rightButtonPressed = mouseEvent.Pressed;
            }
        }

        if (@event is InputEventMouseMotion mouseMotion)
        {
            if (rightButtonPressed)
            {
                // modify accumulated mouse rotation
                rotationX += mouseMotion.Relative.x * LookAroundSpeed;
                rotationY += mouseMotion.Relative.y * LookAroundSpeed;

                // reset rotation
                Transform transform = Transform;
                transform.basis = Basis.Identity;
                Transform = transform;

                RotateObjectLocal(Vector3.Up, rotationX); // first rotate about Y
                RotateObjectLocal(Vector3.Right, rotationY); // then rotate about X
            }
            else if (middlePressed)
            {
                Translation += Transform.basis.x * mouseMotion.Relative.x * -MousePanSpeed;
                Translation += Transform.basis.y * mouseMotion.Relative.y * MousePanSpeed;
            }
        }
    }
}
