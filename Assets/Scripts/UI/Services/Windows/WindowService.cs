using UI.Services.Factory;
using UI.Windows;

namespace UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public WindowBase Open(WindowId windowId)
        {
            WindowBase window = null;
            
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.ChooseBoard:
                    window = _uiFactory.CreateWindow(WindowId.ChooseBoard);
                    break;
                case WindowId.ChoosePlayMode:
                    window = _uiFactory.CreateWindow(WindowId.ChoosePlayMode);
                    break;
            }

            return window;
        }
    }
}