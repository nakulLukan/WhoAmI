using Godot;

public class AudioStreamProvider : Node
{
    [Signal] public delegate void Play(string audioStreamResPath);
    AudioStreamPlayer streamPlayer = null;
    public override void _Ready()
    {
        this.Connect(nameof(Play), this, nameof(OnSoundTrackSelected));
    }
    
    void OnSoundTrackSelected(string audioStreamResPath)
    {
        if (!IsInstanceValid(streamPlayer))
        {
            streamPlayer = new AudioStreamPlayer();
            streamPlayer.VolumeDb = Utility.ToDecibels(0.4F);
            this.AddChild(streamPlayer);
            streamPlayer.Stream = Godot.ResourceLoader.Load<AudioStream>(audioStreamResPath);
            if (streamPlayer.Stream is AudioStreamMP3 audioStream)
            {
                audioStream.Loop = false;
            }
            streamPlayer.Connect(Signals.AudioStreamFinished, this, nameof(PlaybackFinished));
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
