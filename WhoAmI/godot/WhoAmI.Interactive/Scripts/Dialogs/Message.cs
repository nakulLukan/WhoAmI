using System.Collections.Generic;
public static class Message
{
    static int lastAccessedPage = -1;
    public static IDictionary<int, string> DialogMessages = new Dictionary<int, string>(){
        { 1, "WELCOME...                     " },
        { 2, "Please roam around to find more about me." }
    };

    public static string GetNextPageText(int pageNo){
        if(DialogMessages.ContainsKey(pageNo)){
            lastAccessedPage = pageNo;
            return DialogMessages[lastAccessedPage];
        }else {
            if(lastAccessedPage >= 0){
                return DialogMessages[lastAccessedPage]; 
            }
            else{
                return string.Empty;
            }
        }
    }
}
