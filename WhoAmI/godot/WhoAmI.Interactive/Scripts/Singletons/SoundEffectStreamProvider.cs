using Godot;

public class SoundEffectStreamProvider : Node
{
    [Export(PropertyHint.Range)] public float Volume { get; set; } = 1;
    [Signal] public delegate void Play(string audioStreamResPath);
    [Signal] public delegate void PlayFromAudioStream(AudioStream mp3Stream);
    AudioStreamPlayer streamPlayer = null;
    public override void _Ready()
    {
        streamPlayer = new AudioStreamPlayer();
        this.AddChild(streamPlayer);
        this.Connect(nameof(Play), this, nameof(PlayFromPath));
        this.Connect(nameof(PlayFromAudioStream), this, nameof(PlayFromStream));
    }
    
    void PlayFromPath(string audioStreamResPath) =>
        SoundTrackPlay(Godot.ResourceLoader.Load<AudioStream>(audioStreamResPath));

    void PlayFromStream(AudioStream audioStream) =>
        SoundTrackPlay(audioStream);

    void SoundTrackPlay(AudioStream mp3Stream)
    {
        streamPlayer.Stream = mp3Stream;
        streamPlayer.VolumeDb = Utility.ToDecibels(Volume);
        streamPlayer.Play();
    }
}
