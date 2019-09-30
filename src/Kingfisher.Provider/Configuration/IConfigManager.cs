namespace Kingfisher.Provider.Configuration
{
    public interface IConfigManager
    {
        T Get<T>() where T : new();
        void SaveAll();
    }
}