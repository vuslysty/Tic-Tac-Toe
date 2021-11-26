using Enums;
using Infrastructure.Configs;
using UI;
using UI.Services;
using UI.Services.Windows;
using UI.Windows;

namespace Infrastructure.States
{
    public class ChoosePlayModeState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IWindowService _windows;
        private readonly IGameConfig _gameConfig;

        private ChoosePlayModeWindow _choosePlayModeWindow;

        public ChoosePlayModeState(GameStateMachine stateMachine, IWindowService windows, IGameConfig gameConfig)
        {
            _stateMachine = stateMachine;
            _windows = windows;
            _gameConfig = gameConfig;
        }
        
        public void Enter()
        {
            _choosePlayModeWindow = _windows.Open(WindowId.ChoosePlayMode) as ChoosePlayModeWindow;

            if (_choosePlayModeWindow)
            {
                _choosePlayModeWindow.PlayerVsPlayerButton.onClick.AddListener(() =>
                    EnterGameLoopState(GameMode.PlayerVsPlayer));
                _choosePlayModeWindow.PlayerVsBotButton.onClick.AddListener(() =>
                    EnterChooseBotState(GameMode.PlayerVsBot));
                _choosePlayModeWindow.BotVsBotButton.onClick.AddListener(() => EnterChooseBotState(GameMode.BotVsBot));

                _choosePlayModeWindow.BackButton.onClick.AddListener(() => _stateMachine.Enter<ChooseBoardState>());
            }
        }

        public void Exit()
        {
            if (_choosePlayModeWindow)
            {
                _choosePlayModeWindow.Close();
            }
        }

        private void EnterGameLoopState(GameMode gameMode)
        {
            _gameConfig.GameMode = gameMode;
            _stateMachine.Enter<GameLoopState>();
        }
   
        private void EnterChooseBotState(GameMode gameMode)
        {
            _gameConfig.GameMode = gameMode;
            _stateMachine.Enter<ChooseBotState>();
        }
    }
}