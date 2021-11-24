using Infrastructure.AssetManagement;
using Infrastructure.Configs;
using Infrastructure.Services.StaticData;
using UI;
using UI.Services;
using UI.Services.Windows;
using UI.Windows;

namespace Infrastructure.States
{
    public class ChooseBoardState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IWindowService _windows;
        private readonly IStaticDataService _staticData;
        
        private ChooseBoardWindow _chooseBoardWindow;

        public ChooseBoardState(GameStateMachine stateMachine, IWindowService windows, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _windows = windows;
            _staticData = staticData;
        }

        public void Enter()
        {
            _chooseBoardWindow = _windows.Open(WindowId.ChooseBoard) as ChooseBoardWindow;

            if (_chooseBoardWindow)
            {

                _chooseBoardWindow.ThreeButton.onClick.AddListener(() => SetBoardSize(3));
                _chooseBoardWindow.FiveButton.onClick.AddListener(() => SetBoardSize(5));
                _chooseBoardWindow.EightButton.onClick.AddListener(() => SetBoardSize(8));
                _chooseBoardWindow.TenButton.onClick.AddListener(() => SetBoardSize(10));

                _chooseBoardWindow.AddListenersForAllButtons(() => _stateMachine.Enter<ChoosePlayModeState>());
            }
        }

        public void Exit()
        {
            if (_chooseBoardWindow)
            {
                _chooseBoardWindow.Close();
            }
        }

        private void SetBoardSize(int size)
        {
            GameConfig config = _staticData.GetGameConfig();
            
            config.Rows = size;
            config.Cols = size;
        }
    }
}