using Godot;
using System;

public class InteractButtonAction : Button
{
    Action<Node, int> OnInteractPressAction;

    DateTimeOffset interactDuration;
    Node actionNode;
    string _actionName = "ExecuteAction";
    public override void _Input(InputEvent @event)
    {
        if (!IsInteractable)
        {
            return;
        }
        
        if (Input.IsActionJustPressed(InputMapAcitionName.Interact))
        {
            OnInteractPressed();
        }

        if (Input.IsActionJustReleased(InputMapAcitionName.Interact))
        {
            OnInteractReleased();
        }
    }

    bool IsInteractable => this.Visible;

    public override void _Ready()
    {
        this.GetUIControlSignal()
            .Connect(nameof(UIControlSignal.ActionAreaExit), this, nameof(OnActionAreaExit));

        this.GetUIControlSignal()
            .Connect(nameof(UIControlSignal.ActionAreaEntered), this, nameof(OnActionAreaEntered));
    }

    public void OnActionAreaEntered(Node actionNode)
    {
         this.Visible = true;
        this.actionNode = actionNode;
        OnInteractPressAction = (node, magnitude) =>
        {
            node.EmitSignal(_actionName, magnitude);
        };
    }

    void OnActionAreaExit()
    {
        this.Visible = false;
    }

    void OnInteractPressed()
    {
        if (OnInteractPressAction != null)
        {
            interactDuration = DateTimeOffset.UtcNow;
        }
    }

    void OnInteractReleased()
    {
        if (OnInteractPressAction != null)
        {
            var diff = (int)((((float)(DateTimeOffset.UtcNow - interactDuration).TotalMilliseconds) / 1000) * 100);
            OnInteractPressAction(actionNode, diff);
        }

        Visible = false;
    }
}
