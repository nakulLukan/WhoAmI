using Godot;
using Godot.Collections;

namespace WhoAmI.Data
{
    public class ActorDialogManager : Node
    {
        private Dictionary<string, string> Dialogs { get; set; }
        public override void _Ready()
        {
            var http = this.GetHttpService<Dictionary<string, string>>();
            http.Get("data/dialogs.json", (json) =>
            {
                Utility.Print("Successfully downloaded 'dialogs.json' file.");
                Dialogs = json;
            });
        }

        public string GetValue(string key)
        {
            if (Dialogs.ContainsKey(key))
            {
                return Dialogs[key];
            }
            
            return "ERROR: MESSAGE NOT CONFIGURED";
        }
    }
}