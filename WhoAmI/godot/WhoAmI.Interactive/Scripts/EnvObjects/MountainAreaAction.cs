using Godot;
public class MountainAreaAction : Node
{
    [Export] public string SoundTrackPath { get; set; } = "res://StaticAssets/SoundTracks/mountain.mp3";
    [Export] public string Description { get; set; } = "MountainIntro";
    [Signal] public delegate void ExecuteAction();
    public override void _Ready()
    {
        this.Connect(nameof(ExecuteAction), this, nameof(OnDialogActionRecieved));
    }

    void OnDialogActionRecieved(int magnitude)
    {
        this.GetGameActionSignal().EmitSignal(nameof(GameActionSignal.SubTitleDialog), Description);
        this.GetAudioStreamProvider().EmitSignal(nameof(AudioStreamProvider.Play), SoundTrackPath);
    }
}
