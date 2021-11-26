using Enums;
using Infrastructure.Configs;
using Infrastructure.Services;
using UI.Services.Windows;
using UI.Windows;

namespace Infrastructure.States
{
    public class ChooseBotState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IWindowService _windows;
        private readonly IGameConfig _gameConfig;

        private ChooseBotWindow _chooseBotWindow;

        public ChooseBotState(GameStateMachine stateMachine, IWindowService windows, IGameConfig gameConfig)
        {
            _stateMachine = stateMachine;
            _windows = windows;
            _gameConfig = gameConfig;
        }
        
        public void Enter()
        {
            _chooseBotWindow = _windows.Open(WindowId.ChooseBot) as ChooseBotWindow;

            if (_chooseBotWindow)
            {
                _chooseBotWindow.Junior.onClick.AddListener(() =>
                    EnterGameLoopState(BotType.Junior));
                _chooseBotWindow.Master.onClick.AddListener(() =>
                    EnterGameLoopState(BotType.Master));
                _chooseBotWindow.Invincible.onClick.AddListener(() => 
                    EnterGameLoopState(BotType.Invincible));

                _chooseBotWindow.BackButton.onClick.AddListener(() => _stateMachine.Enter<ChoosePlayModeState>());
            }
        }

        public void Exit()
        {
            if (_chooseBotWindow)
            {
                _chooseBotWindow.Close();
            }
        }

        private void EnterGameLoopState(BotType botType)
        {
            _gameConfig.BotType = botType;
            _stateMachine.Enter<GameLoopState>();
        }
    }
}