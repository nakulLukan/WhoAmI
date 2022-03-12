namespace WhoAmI.Web.Models;
public class Person
{
    /// <summary>
    /// Full Name of the person
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// About the person
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Relative path to a model starting without '/'.
    /// </summary>
    public string DisplayPicture { get; set; }
}
