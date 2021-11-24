using Infrastructure.Services;
using UI.Windows;

namespace UI.Services.Windows
{
    public interface IWindowService : IService
    {
        WindowBase Open(WindowId windowId);
    }
}