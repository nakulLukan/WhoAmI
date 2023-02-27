using Godot;
using System;
using System.Threading.Tasks;
namespace WhoAmI.Player
{
    public class DialogAction : Node
    {
        public override void _Ready()
        {
            this.GetGameActionSignal()
                .Connect(Signals.DialogActionEvent, this, nameof(OnDialogRecieved));
        }

        public void OnDialogRecieved(string description)
        {
            Utility.Print(description);
        }
    }
}
