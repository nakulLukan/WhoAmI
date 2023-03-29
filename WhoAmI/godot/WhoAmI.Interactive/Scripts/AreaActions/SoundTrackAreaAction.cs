using Godot;

public class SoundTrackAreaAction : Area
{
    [Export] public string SoundTrackPath { get; set; } = "";
    [Signal] delegate void ExecuteAction();

    UIControlSignal _gameActionSignal;
    AudioStreamPlayer streamPlayer = null;
    public override void _Ready()
    {
        _gameActionSignal = this.GetUIControlSignal();
        _gameActionSignal.Connect(nameof(ExecuteAction), this, nameof(OnSoundTrackSelected));
    }

    void OnSoundTrackSelected()
    {
        if (!IsInstanceValid(streamPlayer))
        {
            streamPlayer = new AudioStreamPlayer();
            this.AddChild(streamPlayer);
            streamPlayer.Stream = Godot.ResourceLoader.Load<AudioStream>(SoundTrackPath);
            if (streamPlayer.Stream is AudioStreamMP3 audioStream)
            {
                audioStream.Loop = false;
            }
            streamPlayer.Connect("finished", this, nameof(PlaybackFinished));
        }

        streamPlayer.Play();
    }

    void PlaybackFinished()
    {
        if (streamPlayer.Playing)
        {
            return;
        }

        streamPlayer.QueueFree();
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(UIControlSignal.SoundTrackEntered));
    }

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(UIControlSignal.ActionAreaExit));
    }
}
