using Godot;
using WhoAmI.Data;

public class PlayerDialog : Control
{
    [Export] public bool ShowDialogInitially { get; set; }
    [Export] public float CharacterDelay { get; set; } = 0.05f;

    AnimationPlayer _animationPlayer;

    ColorRect _colorRect = null;
    RichTextLabel _dialogText = null;
    Timer _timer = null;
    ActorDialogManager _dialogManager = null; 

    string fadeInAnimationName = "SlideIn";
    string fadeOutAnimationName = "SlideOut";
    bool IsOpen() => Visible;
    public override void _Ready()
    {
        this.GetGameActionSignal().Connect(nameof(GameActionSignal.SubTitleDialog), this, nameof(OnDialogTextRecieved));
        _colorRect = GetChild<ColorRect>(0);
        _dialogText = _colorRect.GetChild<RichTextLabel>(0);
        _animationPlayer = GetNode<AnimationPlayer>("./AnimationPlayer");
        _timer = GetNode<Timer>("./Timer");
        _timer.WaitTime = CharacterDelay;
        _dialogManager = GetNode<ActorDialogManager>(NodePath.ActorDialogManager);
        _dialogText.BbcodeEnabled = false;
        Visible = false;
    }

    public void OnDialogTextRecieved(string dialogText)
    {
        OpenDialogMessagePanel(dialogText);
    }

    public override void _Input(InputEvent @event)
    {
        if (IsOpen() && Input.IsActionJustPressed(InputMapAcitionName.Interact))
        {
            CloseDialogMessagePanel();
        }
    }

    void OpenDialogMessagePanel(string dialogKey)
    {
        _dialogText.Text = _dialogManager.GetValue(dialogKey);
        _dialogText.VisibleCharacters = 0;
        _timer.Start();

        if (!IsOpen() || _animationPlayer.CurrentAnimation == fadeOutAnimationName)
        {
            _animationPlayer.Play(fadeInAnimationName);
        }
    }

    void CloseDialogMessagePanel()
    {
        _animationPlayer.Play(fadeOutAnimationName);
        _dialogText.Text = string.Empty;
        _timer.Stop();
    }

    public void OnTimerTimeout()
    {
        _dialogText.VisibleCharacters += 1;
        if (_dialogText.VisibleCharacters >= _dialogText.GetTotalCharacterCount())
        {
            _timer.Stop();
        }
    }
}
