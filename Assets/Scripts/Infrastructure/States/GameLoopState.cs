using System;
using System.Threading.Tasks;
using Enums;
using Infrastructure.Configs;
using Infrastructure.Services;
using Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IGameConfig _gameConfig;

        private GameField _gameField;
        private Hud _hud;
        private GameManager _gameManager;

        public GameLoopState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IGameConfig gameConfig)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _gameConfig = gameConfig;
        }

        public void Enter()
        {
            _hud = _gameFactory.CreateHud().GetComponent<Hud>();
            _gameField = _gameFactory.CreateGameField().GetComponent<GameField>();
            
            _gameManager = new GameManager(_gameConfig, _gameFactory, _gameField, OnGameFinish);
            _gameManager.onChangeMovePlayer += _hud.UpdatePlayerMoveText;
            
            _hud.UpdateWinCondition(_gameConfig.GetWinLenght());

            _gameManager.StartGame();
        }

        public void Exit()
        {
            Object.Destroy(_gameField.gameObject);
            Object.Destroy(_hud.gameObject);

            _gameManager = null;
        }

        private void OnGameFinish(Figure figure, bool isBot)
        {
            Debug.Log("Game finish");
            
            _hud.UpdateResultText(figure, isBot);
            
            DelayedCall(5, () => _gameStateMachine.Enter<ChooseBoardState>());
        }

        private async void DelayedCall(float delayInSeconds, Action action)
        {
            await Task.Delay((int)(delayInSeconds * 1000));
            
            action?.Invoke();
        }
    }
}