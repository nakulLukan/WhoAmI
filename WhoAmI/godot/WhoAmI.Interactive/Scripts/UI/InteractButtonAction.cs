using Godot;
using System;

public class InteractButtonAction : Button
{
    Action OnInteractPressAction;
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

    public void OnFootballKickRecieved(Vector3 kickDirection, float power)
    {
        this.Visible = true;
        OnInteractPressAction = () =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerFootballKicked), kickDirection, power);
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
