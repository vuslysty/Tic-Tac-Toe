using Infrastructure.AssetManagement;
using Infrastructure.Services.StaticData;
using UI.Services.Windows;
using UI.StaticData;
using UI.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    class UIFactory : IUIFactory
    {
        private IAssets _assets;
        private IStaticDataService _staticData;
        
        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public WindowBase CreateWindow(WindowId windowId)
        {
            CreateUIRoot();
            
            WindowConfig config = _staticData.ForWindow(windowId);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);

            return window;
        }

        private void CreateUIRoot()
        {
            if (_uiRoot == null)
            {
                _uiRoot = _assets.Instantiate(AssetPath.UIRootPath).transform;
            }
        }
    }
}