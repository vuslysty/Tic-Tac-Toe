using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configs;
using UI.Services.Windows;
using UI.StaticData;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
        private const string StaticDataGameConfigPath = "StaticData/GameConfig";
        
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private GameConfig _gameConfig;

        public void LoadStaticData()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

            _gameConfig = Resources.Load<GameConfig>(StaticDataGameConfigPath);
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;
        }

        public GameConfig GetGameConfig()
        {
            return _gameConfig;
        }
    }
}