using Godot;
using System;

public class InteractButtonAction : Button
{
    Action OnInteractPressAction;
    public override void _Ready()
    {
        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.DialogAction), this, nameof(OnDialogRecieved));
    }

    public void OnDialogRecieved(string description)
    {
        this.Visible = true;
        OnInteractPressAction = () =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerDialog), description);
        };
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
