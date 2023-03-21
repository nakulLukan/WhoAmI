using Godot;
using System;

public class InteractButtonAction : Button
{
    Action OnInteractPressAction;
    public override void _Input(InputEvent @event)
    {
        if (!IsInteractable)
        {
            return;
        }
        if (Input.IsActionJustReleased(InputMapAcitionName.Interact))
        {
            OnInteractPressed();
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
        OnInteractPressAction = () =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerDialog), description);
        };
    }

    public void OnFootballKickRecieved()
    {
        this.Visible = true;
        OnInteractPressAction = () =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerFootballKicked));
        };
    }

    void OnActionAreaExit()
    {
        this.Visible = false;
    }

    public void OnInteractPressed()
    {
        if (OnInteractPressAction != null)
        {
            OnInteractPressAction();
        }

        Visible = false;
    }
}
