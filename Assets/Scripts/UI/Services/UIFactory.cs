using Infrastructure.AssetManagement;
using StaticData;
using UnityEngine;

namespace UI.Services
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

        public ChooseBoardWindow CreateChooseBoard()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.ChooseBoard);

            CreateUIRoot();
            
            ChooseBoardWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ChooseBoardWindow;
            window.Construct();

            return window;
        }
        
        public ChoosePlayModeWindow CreateChoosePlayMode()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.ChoosePlayMode);

            CreateUIRoot();
            
            ChoosePlayModeWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ChoosePlayModeWindow;
            window.Construct();

            return window;
        }

        public void CreateUIRoot()
        {
            if (_uiRoot == null)
            {
                _uiRoot = _assets.Instantiate(AssetPath.UIRootPath).transform;
            }
        }
    }
}