using Godot;
using Godot.Collections;
using WhoAmI.Interactive.Scripts.Shared.Extensions;

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
            throw new System.Exception($"Unknown dialog key '{key}'.");
        }
    }
}