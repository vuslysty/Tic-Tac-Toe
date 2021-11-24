using System;
using UI.Services.Windows;
using UI.Windows;

namespace UI.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}