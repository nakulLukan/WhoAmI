using Godot;
using System;

public class Dialog : RichTextLabel
{
    public int CurrentPage { get; set; } = 1;
    private Timer timer = null;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        VisibleCharacters = 0;
        BbcodeText = Message.DialogMessages[CurrentPage];
        SelectionEnabled = true;
        timer = GetNode<Timer>("../IntroDialogAnimationTimer");
        SetProcessInput(true);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if(@event is InputEventMouseButton eventKey){
            if(eventKey.Pressed && eventKey.ButtonIndex == (int)ButtonList.Left){
                if(VisibleCharacters < GetTotalCharacterCount()){
                    VisibleCharacters = GetTotalCharacterCount();
                }
                else if (CurrentPage < Message.DialogMessages.Count){
                    VisibleCharacters = 0;
                    CurrentPage++;
                    BbcodeText = Message.GetNextPageText(CurrentPage);
                }else{
                    GetParent<Polygon2D>().QueueFree();
                }
            }
        }
    }

    public void _OnIntroDialogAnimationTimerTimeout(){
        if(timer.WaitTime >= 1){
            timer.WaitTime = 0.05f;
        }
        VisibleCharacters += 1;
    }
}
