using Godot;
using System;
public class PlayerMovement : KinematicBody
{
    public int Speed { get; set; } = 4;
    public int RotationSpeed { get; set; } = 7;
    public float PlayerActionTransitionSpeed { get; set; } = 0.1F;


    private KinematicBody body;
    private int forwardAction = 0;
    private int backwardAction = 0;
    private int leftAction = 0;
    private int rightAction = 0;
    private float currentAnimAction = 0;
    private float playerTargetAnimAction = 0;
    private const string ANIM_TREE_MOVEMENT_PARAM = "parameters/movement/blend_position";

    AnimationTree animationTree;
    private Spatial player;
    private Vector3 playerRotationDegrees;
    private Vector3 playerTargetRotationDegrees;
    Vector3 velocity = Vector3.Zero;

    private Vector3 gravity = Vector3.Down * (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
    public override void _Ready()
    {
        body = this;
        player = GetNode("player") as Spatial;
        playerRotationDegrees = player.RotationDegrees;
        playerTargetRotationDegrees = playerRotationDegrees;
        animationTree = GetNode("player").GetNode("AnimationTree") as AnimationTree;
    }

    public override void _Process(float delta)
    {
        playerRotationDegrees.y = Godot.Mathf.Rad2Deg(Godot.Mathf.LerpAngle(
            Godot.Mathf.Deg2Rad(playerRotationDegrees.y), 
            Godot.Mathf.Deg2Rad(playerTargetRotationDegrees.y),
            RotationSpeed * delta));
        player.RotationDegrees = playerRotationDegrees;

        playerTargetAnimAction = Math.Min(1, forwardAction + backwardAction + leftAction + rightAction);
        if (playerTargetAnimAction != currentAnimAction){
            currentAnimAction = Godot.Mathf.Lerp(currentAnimAction, playerTargetAnimAction, PlayerActionTransitionSpeed);
            animationTree.Set(ANIM_TREE_MOVEMENT_PARAM, currentAnimAction);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        velocity += gravity;
        velocity.x = (rightAction - leftAction) * Speed;
        velocity.z = (backwardAction - forwardAction) * Speed;
        velocity = body.MoveAndSlide(velocity, Vector3.Up);
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey eventKey){
            bool isPressed = @event.IsPressed();
            switch((KeyList)eventKey.Scancode){
                case KeyList.W:
                    if(isPressed){
                        forwardAction = 1;
                        playerTargetRotationDegrees.y = 180;
                    }else{
                        forwardAction = 0;
                    }
                    break;
                case KeyList.A:
                    if(isPressed){
                        leftAction = 1;
                        playerTargetRotationDegrees.y = -90;
                    }else{
                        leftAction = 0;
                    }
                    break;
                case KeyList.S:
                    if(isPressed){
                        backwardAction = 1;
                        playerTargetRotationDegrees.y = 0;
                    }else{
                        backwardAction = 0;
                    }
                    break;
                case KeyList.D:
                    if(isPressed){
                        rightAction = 1;
                        playerTargetRotationDegrees.y = 90;
                    }else{
                        rightAction = 0;
                    }
                    break;
            }
        }
    }
}
