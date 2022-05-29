using Godot;
using System;
public class PlayerMovement : KinematicBody
{
    public int Speed { get; set; } = 5;
    public int RotationSpeed { get; set; } = 7;
    private KinematicBody body;
    private int forwardAction = 0;
    private int backwardAction = 0;
    private int leftAction = 0;
    private int rightAction = 0;

    private Spatial player;
    private Vector3 playerRotationDegrees;
    private Vector3 playerTargetRotationDegrees;
    
    public override void _Ready()
    {
        body = this;
        player = GetNode("player") as Spatial;
        playerRotationDegrees = player.RotationDegrees;
        playerTargetRotationDegrees = playerRotationDegrees;
    }

    public override void _Process(float delta)
    {
        Vector3 playerMovement = Vector3.Zero;
        playerMovement.x = rightAction - leftAction;
        playerMovement.z = backwardAction - forwardAction;
        this.Translation += playerMovement * Speed * delta;
        playerRotationDegrees.y = Godot.Mathf.Rad2Deg(Godot.Mathf.LerpAngle(
            Godot.Mathf.Deg2Rad(playerRotationDegrees.y), 
            Godot.Mathf.Deg2Rad(playerTargetRotationDegrees.y),
            RotationSpeed * delta));
        player.RotationDegrees = playerRotationDegrees;
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey eventKey){
            switch((KeyList)eventKey.Scancode){
                case KeyList.W:
                    forwardAction = @event.IsPressed() ? 1 : 0;
                    playerTargetRotationDegrees.y = 180;
                    break;
                case KeyList.A:
                    leftAction = @event.IsPressed() ? 1 : 0;
                    playerTargetRotationDegrees.y = -90;
                    break;
                case KeyList.S:
                    backwardAction = @event.IsPressed() ? 1 : 0;
                    playerTargetRotationDegrees.y = 0;
                    break;
                case KeyList.D:
                    rightAction = @event.IsPressed() ? 1 : 0;
                    playerTargetRotationDegrees.y = 90;
                    break;
            }
        }
    }
}
