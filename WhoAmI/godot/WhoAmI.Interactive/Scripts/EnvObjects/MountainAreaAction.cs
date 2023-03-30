using Godot;

public class MountainAreaAction : Node
{
    [Export] public string SoundTrackPath { get; set; } = "res://StaticAssets/SoundTracks/mountain.mp3";
    [Export] public string Description { get; set; } = "MountainIntro";
    [Signal] public delegate void ExecuteAction();
    AudioStreamPlayer streamPlayer = null;
    public override void _Ready()
    {
        this.Connect(nameof(ExecuteAction), this, nameof(OnDialogActionRecieved));
    }

    void OnDialogActionRecieved(int magnitude)
    {
        this.GetGameActionSignal().EmitSignal(nameof(GameActionSignal.SubTitleDialog), Description);
        OnSoundTrackSelected(magnitude);
    }
    void OnSoundTrackSelected(int magnitude)
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
}
