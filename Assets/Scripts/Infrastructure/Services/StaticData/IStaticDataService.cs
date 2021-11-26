using Infrastructure.Configs;
using UI.Services.Windows;
using UI.StaticData;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        WindowConfig ForWindow(WindowId windowId);
    }
}