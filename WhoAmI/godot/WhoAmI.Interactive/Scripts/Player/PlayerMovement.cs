using Godot;
using System;
public class PlayerMovement : KinematicBody
{
    // Movement speed of the player
    public int Speed { get; set; } = 8;

    // Rotation speed of the player
    public int RotationSpeed { get; set; } = 7;

    // Player animation transition speed
    public float PlayerActionTransitionSpeed { get; set; } = 0.3F;

    #region Movement Keys
    private int forwardAction = 0;
    private int backwardAction = 0;
    private int leftAction = 0;
    private int rightAction = 0;
    #endregion

    // Player current animation
    private float currentAnimAction = 0;

    // Player target animation
    private float playerTargetAnimAction = 0;

    // Path to player movement animation tree blend parameter
    private const string ANIM_TREE_MOVEMENT_PARAM = "parameters/movement/blend_position";

    // Reference to animation tree node of the player.
    AnimationTree animationTree;

    // Reference to player node
    private Spatial player;

    // Player velocity
    Vector3 velocity = Vector3.Zero;

    // User movement key input vector.
    Vector3 input = Vector3.Zero;

    // Player target rotation
    Vector3 targetLookDir = Vector3.Zero;

    // Player current rotation
    Vector3 currentLookDir = Vector3.Zero;

    private Vector3 gravity = Vector3.Down * (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
    public override void _Ready()
    {
        player = GetNode("player") as Spatial;
        animationTree = GetNode("player").GetNode("AnimationTree") as AnimationTree;
    }

    public override void _Process(float delta)
    {
        // Play run animation if any movement keys are pressed.
        // 1 for run animation
        // 0 for idle animation
        playerTargetAnimAction = Math.Min(1, forwardAction + backwardAction + leftAction + rightAction);

        // Make the animation transition smooth using lerp()
        if (playerTargetAnimAction != currentAnimAction){
            currentAnimAction = Godot.Mathf.Lerp(currentAnimAction, playerTargetAnimAction, PlayerActionTransitionSpeed);
            animationTree.Set(ANIM_TREE_MOVEMENT_PARAM, currentAnimAction);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        // Get linear velocity of from the movement key press.
        velocity.x = (rightAction - leftAction);
        velocity.z = (backwardAction - forwardAction);
        input.x = velocity.x;
        input.z = velocity.z;
        
        // Normalize direction vector for uniform speed in all direction
        velocity = velocity.Normalized() * Speed;
        velocity.y = gravity.y;
        // Update player target roation only if any movement key (W,A,S or D) is pressed.
        if(input.Length() > 0){
            targetLookDir = Transform.Identity.LookingAt(Vector3.Zero - input.Normalized(), Vector3.Up).basis.GetEuler();
        }
        
        // Update rotation till player current looking direction not equal to target looking direction.
        if(currentLookDir != targetLookDir){
            // Lerp the radians for smooth transition
            currentLookDir.x = Godot.Mathf.LerpAngle(currentLookDir.x, targetLookDir.x, 0.3f);
            currentLookDir.y = Godot.Mathf.LerpAngle(currentLookDir.y, targetLookDir.y, 0.3f);
            player.Rotation = currentLookDir;
        }
        
        // Move the kinematic body
        velocity = MoveAndSlide(velocity, Vector3.Up);
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey eventKey){
            bool isPressed = @event.IsPressed();
            switch((KeyList)eventKey.Scancode){
                case KeyList.W:
                    forwardAction = isPressed ? 1 : 0;
                    break;
                case KeyList.A:
                    leftAction = isPressed ? 1 : 0;
                    break;
                case KeyList.S:
                    backwardAction = isPressed ? 1 : 0;
                    break;
                case KeyList.D:
                    rightAction = isPressed ? 1 : 0;
                    break;
            }
        }
    }
}
