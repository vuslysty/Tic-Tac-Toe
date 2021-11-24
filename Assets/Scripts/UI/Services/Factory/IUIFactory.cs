using Infrastructure.Services;
using UI.Services.Windows;
using UI.Windows;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        WindowBase CreateWindow(WindowId windowId);
    }
}