using System;
using System.Threading.Tasks;
using Enums;
using Infrastructure.Services;
using Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameMode>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        
        private GameField _gameField;
        private Hud _hud;
        private GameManager _gameManager;

        public GameLoopState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter(GameMode gameMode)
        {
            _hud = _gameFactory.CreateHud().GetComponent<Hud>();
            _gameField = _gameFactory.CreateGameField().GetComponent<GameField>();
            
            _gameManager = new GameManager(gameMode, _gameField, OnGameFinish);
            _gameManager.onChangeMovePlayer += _hud.UpdatePlayerMoveText;
            
            _hud.UpdateWinCondition(_gameManager.GetWinLenght());

            _gameManager.StartGame();
        }

        public void Exit()
        {
            Object.DestroyImmediate(_gameField.gameObject);
            Object.DestroyImmediate(_hud.gameObject);

            _gameManager = null;
        }

        private void OnGameFinish(Figure figure, bool isBot)
        {
            Debug.Log("Game finish");
            
            _hud.UpdateResultText(figure, isBot);
            
            DelayedCall(3, () => _gameStateMachine.Enter<ChooseBoardState>());
        }

        private async void DelayedCall(float delayInSeconds, Action action)
        {
            await Task.Delay((int)(delayInSeconds * 1000));
            
            action?.Invoke();
        }
    }
}