using Infrastructure.Services;

namespace UI.Services
{
    public interface IUIFactory : IService
    {
        ChooseBoardWindow CreateChooseBoard();
        ChoosePlayModeWindow CreateChoosePlayMode();
    }
}