using System;

namespace WhoAmI.Services.RestService
{
    public interface IHttpService<T>
    {
        void Get(string relativeApiPath, Action<T> callback);
    }
}