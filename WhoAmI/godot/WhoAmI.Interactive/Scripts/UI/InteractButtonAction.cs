using Godot;
using System;

public class InteractButtonAction : Button
{
    Action<int> OnInteractPressAction;

    DateTimeOffset interactDuration;
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
        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.ActionAreaExit), this, nameof(OnActionAreaExit));

        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.DialogAction), this, nameof(OnDialogRecieved));
        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.FootballAction), this, nameof(OnFootballKickRecieved));
    }

    public void OnDialogRecieved(string description)
    {
        this.Visible = true;
        OnInteractPressAction = (magnitude) =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerDialog), description);
        };
    }

    public void OnFootballKickRecieved()
    {
        this.Visible = true;
        OnInteractPressAction = (magnitude) =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerFootballKicked), magnitude);
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
            OnInteractPressAction(diff);
        }

        Visible = false;
    }
}
