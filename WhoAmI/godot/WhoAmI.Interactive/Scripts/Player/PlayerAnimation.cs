using Godot;
using System;
public class PlayerAnimation : Spatial
{
    // Player animation transition speed
    public float PlayerActionTransitionSpeed { get; set; } = 0.3F;

    // Player current animation
    private float currentAnimAction = 0;

    // Player target animation
    private float playerTargetAnimAction = 0;

    // Path to player movement animation tree blend parameter
    private const string ANIM_TREE_MOVEMENT_PARAM = "parameters/movement/blend_position";

    // Reference to animation tree node of the player.
    AnimationTree animationTree;

    float runningSpeed = 0;
    public override void _Ready()
    {
        animationTree = GetNode<AnimationTree>("AnimationTree");
    }

    public override void _Process(float delta)
    {
        // Play run animation if any movement keys are pressed.
        // 1 for run animation
        // 0 for idle animation
        playerTargetAnimAction = Math.Min(1, runningSpeed);

        // Make the animation transition smooth using lerp()
        if (playerTargetAnimAction != currentAnimAction){
            currentAnimAction = Godot.Mathf.Lerp(currentAnimAction, playerTargetAnimAction, PlayerActionTransitionSpeed);
            animationTree.Set(ANIM_TREE_MOVEMENT_PARAM, currentAnimAction);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey eventKey){
            bool isPressed = @event.IsPressed();
            switch((KeyList)eventKey.Scancode){
                case KeyList.W:
                    runningSpeed = isPressed ? 1 : 0;
                    break;
                case KeyList.S:
                    runningSpeed = isPressed ? 1 : 0;
                    break;
            }
        }
    }
}
