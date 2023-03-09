using Godot;
using System;

public class PlayerDialog : Control
{
    [Export] public bool ShowDialogInitially { get; set; }
    AnimationPlayer _animationPlayer;

    ColorRect _colorRect = null;
    RichTextLabel _dialogText = null;
    public override void _Ready()
    {
        this.GetUIControlSignal().Connect(nameof(UIControlSignal.PlayerDialog), this, nameof(OnDialogTextRecieved));
        _colorRect = GetChild<ColorRect>(0);
        _dialogText = _colorRect.GetChild<RichTextLabel>(0);
        _animationPlayer = GetNode<AnimationPlayer>("./AnimationPlayer");
        Visible = false;
    }

    public void OnDialogTextRecieved(string dialogText)
    {
        OpenDialogMessagePanel(dialogText);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            switch ((ButtonList)mouseButtonEvent.ButtonIndex)
            {
                case ButtonList.Left:
                    if (Visible)
                    {
                        CloseDialogMessagePanel();
                    }
                    break;
            }
        }
    }

    void OpenDialogMessagePanel(string dialogText)
    {
        _dialogText.Text = dialogText;
        _animationPlayer.Play("SlideIn");
    }

    private void CloseDialogMessagePanel()
    {
        _animationPlayer.Play("SlideOut");
        _dialogText.Text = string.Empty;
    }
}
