using Godot;
using System;
using System.Collections.Generic;

public class PlayerAction : Spatial
{
    public float PlayerActionTransitionSpeed { get; set; } = 0.1F;
    private float currentAction = 0;
    private float playerTargetAction = 0;

    private int forwardAction = 0;
    private int backwardAction = 0;
    private int leftAction = 0;
    private int rightAction = 0;

    private const string ANIM_TREE_MOVEMENT_PARAM = "parameters/movement/blend_position";
    AnimationTree animationTree;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationTree = (GetNode("AnimationTree") as AnimationTree);
    }
    
    public override void _Process(float delay)
    {
        playerTargetAction = Math.Min(1, forwardAction + backwardAction + leftAction + rightAction);
        if(playerTargetAction != currentAction){
            currentAction = Godot.Mathf.Lerp(currentAction, playerTargetAction, PlayerActionTransitionSpeed);
            animationTree.Set(ANIM_TREE_MOVEMENT_PARAM, currentAction);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey eventKey){
            switch((KeyList)eventKey.Scancode){
                case KeyList.W:
                    forwardAction = @event.IsPressed() ? 1 : 0;
                    break;
                case KeyList.A:
                    leftAction = @event.IsPressed() ? 1 : 0;
                    break;
                case KeyList.S:
                    backwardAction = @event.IsPressed() ? 1 : 0;
                    break;
                case KeyList.D:
                    rightAction = @event.IsPressed() ? 1 : 0;
                    break;
            }
        }
    }
}
