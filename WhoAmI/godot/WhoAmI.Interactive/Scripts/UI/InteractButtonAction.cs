using Godot;
using System;

public class InteractButtonAction : Button
{
    Action OnInteractPressAction;
    public override void _Ready()
    {
        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.DialogAction), this, nameof(OnDialogRecieved));
        this.GetGameActionSignal()
            .Connect(nameof(GameActionSignal.DialogActionAreaExit), this, nameof(OnDialogAreaExit));
            
    }

    public void OnDialogRecieved(string description)
    {
        this.Visible = true;
        OnInteractPressAction = () =>
        {
            this.GetUIControlSignal().EmitSignal(nameof(UIControlSignal.PlayerDialog), description);
        };
    }

    void OnDialogAreaExit(){
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
