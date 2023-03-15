using Godot;
using Newtonsoft.Json;
using System;
using System.Text;

namespace WhoAmI.Services.RestService.Providers
{
    public class HttpService<T> : Node, IHttpService<T>
    {
        Action<T> action;

        public void Get(string relativeApiPath, Action<T> callback)
        {
            HTTPRequest request = new HTTPRequest();
            AddChild(request);
            request.Connect("request_completed", this, nameof(OnRequestCompleted));
            string address = $"{AppConstant.HttpHostAddress}/{relativeApiPath}";
            var error = request.Request(address, new string[] { }, false, HTTPClient.Method.Get, string.Empty);
            if (error != Error.Ok)
            {
                Utility.PrintError($"HTTP: '{relativeApiPath}' failed with error: {error}");
                QueueFree();
            }
            action = callback;
        }

        public void OnRequestCompleted(int result, int response_code, string[] headers, byte[] body)
        {
            action(JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body)));
            QueueFree();
        }
    }
}