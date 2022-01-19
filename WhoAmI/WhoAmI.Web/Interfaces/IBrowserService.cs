namespace WhoAmI.Web.Interfaces;
public interface IBrowserService
{
    /// <summary>
    /// Get document body scroll top level
    /// </summary>
    /// <returns></returns>
    Task<int> GetBodyScrollTop();

    /// <summary>
    /// Get document element scroll top level
    /// </summary>
    /// <returns></returns>
    Task<int> GetElementScrollTop();

}
