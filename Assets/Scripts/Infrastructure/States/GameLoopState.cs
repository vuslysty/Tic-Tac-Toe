using Enums;
using Infrastructure.Services;
using Logic;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameMode>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        
        private GameField _gameField;

        public GameLoopState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter(GameMode gameMode)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            _gameField = _gameFactory.CreateGameField().GetComponent<GameField>();
        }

        public void Exit()
        {
        }
    }
}