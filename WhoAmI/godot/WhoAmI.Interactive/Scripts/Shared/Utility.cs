public static class Utility{

    public static void Print(object input){
        System.Diagnostics.Debug.Print(input.ToString());
    }

    public static void PrintPretty(object input){
        System.Diagnostics.Debug.Print(Newtonsoft.Json.JsonConvert.SerializeObject(input.ToString(), Newtonsoft.Json.Formatting.Indented));
    }
}