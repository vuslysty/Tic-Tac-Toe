using Configs;
using Infrastructure.Services;
using StaticData;
using UI.Services;

namespace Infrastructure.AssetManagement
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        WindowConfig ForWindow(WindowId windowId);
        GameConfig GetGameConfig();
    }
}