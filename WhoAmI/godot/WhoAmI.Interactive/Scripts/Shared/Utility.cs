public static class Utility{

    public static void Print(object input){
        System.Diagnostics.Debug.Print(input.ToString());
    }

    public static void PrintError(object input){
        System.Diagnostics.Debug.Print($"ERR: {input}");
    }

    public static void PrintPretty(object input){
        System.Diagnostics.Debug.Print(Newtonsoft.Json.JsonConvert.SerializeObject(input.ToString(), Newtonsoft.Json.Formatting.Indented));
    }

    public static float ToDecibels(float factor){
        return 10 * Godot.Mathf.Log(factor);
    }
}