using UI;
using UI.Services;

namespace Infrastructure.States
{
    public class ChoosePlayModeState : IState
    {
        private GameStateMachine _stateMachine;
        private IUIFactory _uiFactory;
        
        private ChoosePlayModeWindow _choosePlayModeWindow;

        public ChoosePlayModeState(GameStateMachine stateMachine, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            _choosePlayModeWindow = _uiFactory.CreateChoosePlayMode();
            
            _choosePlayModeWindow.PlayerVsPlayerButton.onClick.AddListener(() => _stateMachine.Enter<GameLoopState, GameMode>(GameMode.PlayerVsPlayer));
            _choosePlayModeWindow.PlayerVsBotButton.onClick.AddListener(() => _stateMachine.Enter<GameLoopState, GameMode>(GameMode.PlayerVsBot));
            _choosePlayModeWindow.BotVsBotButton.onClick.AddListener(() => _stateMachine.Enter<GameLoopState, GameMode>(GameMode.BotVsBot));
        }

        public void Exit()
        {
            if (_choosePlayModeWindow)
            {
                _choosePlayModeWindow.Close();
            }
        }
    }
}