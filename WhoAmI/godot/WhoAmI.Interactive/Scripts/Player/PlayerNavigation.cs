using Godot;
using System;

public class PlayerNavigation : KinematicBody
{
    [Export] public float GravitationalForce { get; set; } = 9.8F;
    [Export] public float MovementSpeed { get; set; } = 0.2F;
    [Export] public float RotationSpeed { get; set; } = 10F;

    Spatial gravity = null;
    Spatial subPlayer = null;
    float time = 0;
    
    Vector2 keyboardAction = Vector2.Zero;

    bool isCameraInActive = false;
    
    public override void _Ready()
    {
        gravity = GetTree().Root.GetNode<Spatial>(NodePath.GravityPoint);
        subPlayer = GetChild<Spatial>(4);
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = gravity.GlobalTransform.origin - GlobalTransform.origin;

        // Add gravitational force
        MoveAndCollide(direction * delta * GravitationalForce);

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
    }

    public void IsFreeLook(bool cameraInActive)
    {
        isCameraInActive = cameraInActive;
    }
}
