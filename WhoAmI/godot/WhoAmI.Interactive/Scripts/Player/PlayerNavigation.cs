using Godot;
using System;

public class PlayerNavigation : KinematicBody
{
    [Export] public int BodyMass { get; set; } = 10;
    [Export] public float GravitationalForce { get; set; } = 9.8F;
    [Export] public float MovementSpeed { get; set; } = 0.2F;
    [Export] public float RotationSpeed { get; set; } = 10F;
    [Export] public int MaxZoomOut { get; set; } = 35;
    [Export] public int MinZoomOut { get; set; } = 15;
    [Export] public float ZoomSpeed { get; set; } = 0.4f;

    Spatial gravity = null;
    Spatial subPlayer = null;
    Spatial cameraPositionTarget;
    float time = 0;

    Vector2 keyboardAction = Vector2.Zero;

    bool isCameraInActive = false;

    public override void _Ready()
    {
        gravity = GetTree().Root.GetNode<Spatial>(NodePath.GravityPoint);
        subPlayer = GetNode<Spatial>(NodePath.SubPlayer);
        cameraPositionTarget = GetNode<Spatial>(NodePath.PlayerCameraPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = gravity.GlobalTransform.origin - GlobalTransform.origin;

        // Add gravitational force
        var gravityForce = this.CalculateGravity(BodyMass, gravity.GlobalTransform.origin, GameConstant.EarthMass);
        MoveAndCollide(direction * delta * gravityForce);

        // Rotate the body normal to the earth surface
        this.SafeLookAt(gravity.GlobalTransform.origin, Vector3.Up);

        keyboardAction = keyboardAction.Normalized();
        // Move y-axis
        if (keyboardAction.y != 0)
        {
            MoveAndCollide(subPlayer.GlobalTransform.basis.z.Normalized() * keyboardAction.y * MovementSpeed * delta);
        }

        // Move x-axis
        if (keyboardAction.x != 0)
        {
            subPlayer.RotateObjectLocal(Vector3.Up, -keyboardAction.x * delta * RotationSpeed);
            // MoveAndCollide(Transform.basis.x * keyboardAction.x * MovementSpeed);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            bool isPressed = @event.IsPressed();
            switch ((KeyList)eventKey.Scancode)
            {
                case KeyList.W:
                    keyboardAction.y = isPressed ? 1 : 0;
                    break;
                case KeyList.S:
                    keyboardAction.y = isPressed ? -1 : 0;
                    break;
                case KeyList.A:
                    keyboardAction.x = isPressed ? -1 : 0;
                    break;
                case KeyList.D:
                    keyboardAction.x = isPressed ? 1 : 0;
                    break;
            }
        }

        if (@event is InputEventMouseButton mouseEvent)
        {
            switch ((ButtonList)mouseEvent.ButtonIndex)
            {
                case ButtonList.WheelDown:
                    ZoomCamera(1);
                    break;
                case ButtonList.WheelUp:
                    ZoomCamera(-1);
                    break;
            }
        }
    }

    private void ZoomCamera(int zoomDirection)
    {
        // Move camera target position forward or backward.
        cameraPositionTarget.GlobalTranslate(cameraPositionTarget.GlobalTransform.basis.z * ZoomSpeed * zoomDirection);
        if (cameraPositionTarget.Translation.z < MinZoomOut || cameraPositionTarget.Translation.z > MaxZoomOut)
        {
            var z = cameraPositionTarget.Translation.z - Math.Max(MinZoomOut, Math.Min(cameraPositionTarget.Translation.z, MaxZoomOut));
            cameraPositionTarget.Translation += Vector3.Forward * z;
        }
    }

    public void IsFreeLook(bool cameraInActive)
    {
        isCameraInActive = cameraInActive;
    }
}
