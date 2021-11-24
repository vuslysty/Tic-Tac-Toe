using Configs;
using Infrastructure.AssetManagement;
using UI;
using UI.Services;

namespace Infrastructure.States
{
    public class ChooseBoardState : IState
    {
        private readonly GameStateMachine _stateMachine;
        
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticData;
        
        private ChooseBoardWindow _chooseBoardWindow;

        public ChooseBoardState(GameStateMachine stateMachine, IUIFactory uiFactory, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _staticData = staticData;
        }

        public void Enter()
        {
            _chooseBoardWindow = _uiFactory.CreateChooseBoard();
            
            GameConfig config = _staticData.GetGameConfig();
            
            _chooseBoardWindow.ThreeButton.onClick.AddListener(() =>
            {
                config.Rows = 3;
                config.Cols = 3;
            });

            _chooseBoardWindow.FiveButton.onClick.AddListener(() =>
            {
                config.Rows = 5;
                config.Cols = 5;
            });
            
            _chooseBoardWindow.EightButton.onClick.AddListener(() =>
            {
                config.Rows = 8;
                config.Cols = 8;
            });

            _chooseBoardWindow.TenButton.onClick.AddListener(() =>
            {
                config.Rows = 10;
                config.Cols = 10;
            });
            
            _chooseBoardWindow.AddListenersForAllButtons(() => _stateMachine.Enter<ChoosePlayModeState>());
        }

        public void Exit()
        {
            if (_chooseBoardWindow)
            {
                _chooseBoardWindow.Close();
            }
        }
    }
}