namespace CSWPFTemplate.Settings
{
    //if data access is implemented in a separate library, this interface can be moved there
    //to allow initializiation with settings from the implementation in the wpf level
    public interface IBaseSettings
    {
        string LocalDataDirectory { get; }
    }
}
