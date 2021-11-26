using System.Collections.Generic;
using Enums;
using Infrastructure.Configs;
using Infrastructure.Services;
using Random = UnityEngine.Random;

namespace Logic
{
    public class GameManager
    {
        private readonly IGameConfig _gameConfig;
        private readonly IGameFactory _gameFactory;
        private readonly GameField _gameField;
        private readonly int _winLenght;

        private IPlayer _playerX;
        private IPlayer _playerO;

        public delegate void OnGameFinish(Figure figure, bool isBot = false);
        private readonly OnGameFinish _onGameFinish;

        public delegate void OnChangeMovePlayer(Figure figure, bool isBot = false);
        public OnChangeMovePlayer onChangeMovePlayer;
        
        public GameManager(IGameConfig gameConfig, IGameFactory gameFactory, GameField gameField, OnGameFinish onGameFinish)
        {
            _gameConfig = gameConfig;
            _gameFactory = gameFactory;
            _gameField = gameField;
            _onGameFinish = onGameFinish;
            
            _winLenght = gameConfig.GetWinLenght();

            CreatePlayers(gameConfig.GameMode, gameField);
        }

        public async void StartGame()
        {
            IPlayer currentPlayer = null;

            while (true)
            {
                currentPlayer = GetNextPlayer(currentPlayer);

                onChangeMovePlayer?.Invoke(currentPlayer.Figure, currentPlayer is IBot);
                
                CellPosition position = await currentPlayer.GetRightToMove();

                if (CheckGameFinish(position, currentPlayer))
                {
                    break;
                }
            }
        }

        private bool CheckGameFinish(CellPosition position, IPlayer currentPlayer)
        {
            if (IsWin(position, currentPlayer, out RowType rowType))
            {
                List<Cell> winCells = _gameField.RealChecker.GetCellsOnRow(position, currentPlayer.Figure, rowType);

                foreach (Cell winCell in winCells)
                {
                    CellBehaviour cellBehaviour = _gameField.GetCellBehaviour(winCell);

                    if (cellBehaviour)
                    {
                        cellBehaviour.PlayAnimation();
                    }
                }
                
                _onGameFinish?.Invoke(currentPlayer.Figure, currentPlayer is IBot);
                return true;
            }

            if (_gameField.CountOfEmptyCells() == 0)
            {
                foreach (Cell cell in _gameField.Grid)
                {
                    _gameField.GetCellBehaviour(cell).PlayAnimation();
                }
                
                _onGameFinish?.Invoke(Figure.NONE);
                return true;
            }

            return false;
        }

        private void CreatePlayers(GameMode gameMode, GameField gameField)
        {
            switch (gameMode)
            {
                case GameMode.PlayerVsPlayer:
                    _playerX = new Player(gameField, Figure.CROSS);
                    _playerO = new Player(gameField, Figure.NOUGHT);
                    break;
                
                case GameMode.PlayerVsBot:
                    if (Random.value > 0.5f)
                    {
                        _playerX = new Player(gameField, Figure.CROSS);
                        _playerO = _gameFactory.CreateBot(_gameConfig.BotType, gameField, Figure.NOUGHT);
                    }
                    else
                    {
                        _playerX = _gameFactory.CreateBot(_gameConfig.BotType, gameField, Figure.CROSS);
                        _playerO = new Player(gameField, Figure.NOUGHT);
                    }
                    
                    break;
                
                case GameMode.BotVsBot:
                    _playerX = _gameFactory.CreateBot(_gameConfig.BotType, gameField, Figure.CROSS);
                    _playerO = _gameFactory.CreateBot(_gameConfig.BotType, gameField, Figure.NOUGHT);
                    break;
            }
        }

        private bool IsWin(CellPosition position, IPlayer currentPlayer, out RowType rowType)
        {
            return _gameField.RealChecker.GetLenght(position, currentPlayer.Figure, out rowType) >= _winLenght;
        }

        private IPlayer GetNextPlayer(IPlayer currentPlayer)
        {
            IPlayer nextPlayer = null;
            
            if (currentPlayer == null)
            {
                nextPlayer = _playerX;
            }
            else if (currentPlayer == _playerX)
            {
                nextPlayer = _playerO;
            }
            else
            {
                nextPlayer = _playerX;
            }

            return nextPlayer;
        }
    }
}