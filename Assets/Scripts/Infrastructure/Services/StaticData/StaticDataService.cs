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

        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void LoadStaticData()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;
        }
    }
}