namespace WhoAmI.Web.Interfaces;

public interface IApiService
{
    public Task<T> Get<T>(string path);
}