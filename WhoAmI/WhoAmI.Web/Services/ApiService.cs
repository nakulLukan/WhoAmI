using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using WhoAmI.Web.Interfaces;

namespace WhoAmI.Web.Services;
public class ApiService : IApiService
{
    public readonly string DataBasePath;

    /// <summary>
    /// Service to fetch data from local json file.
    /// </summary>
    /// <param name="configuration"></param>
    public ApiService(NavigationManager navigationManager)
    {
        DataBasePath = $"{navigationManager.BaseUri}data/";
    }

    /// <summary>
    /// Get data from json file
    /// </summary>
    /// <typeparam name="T">Expected return type</typeparam>
    /// <param name="path">Relative path to the json file</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<T> Get<T>(string path)
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync($"{DataBasePath}{path}");
        return JsonConvert.DeserializeObject<T>(content) ?? throw new ArgumentNullException(content);
    }
}
