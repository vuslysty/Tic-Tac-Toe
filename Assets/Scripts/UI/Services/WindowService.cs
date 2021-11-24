using System;

namespace UI.Services
{
    public class WindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.ChooseBoard:
                    break;
                case WindowId.ChoosePlayMode:
                    break;
                case WindowId.GameField:
                    break;
            }
        }
    }
}