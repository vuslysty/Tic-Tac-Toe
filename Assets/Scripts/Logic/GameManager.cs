using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
    public class GameManager
    {
        private readonly GameField _gameField;
        private readonly int _winLenght;

        private IPlayer _playerX;
        private IPlayer _playerO;

        public delegate void OnGameFinish(Figure figure, bool isBot = false);
        private readonly OnGameFinish _onGameFinish;

        public delegate void OnChangeMovePlayer(Figure figure, bool isBot = false);
        public OnChangeMovePlayer onChangeMovePlayer;
        
        public GameManager(GameMode gameMode, GameField gameField, OnGameFinish onGameFinish)
        {
            _gameField = gameField;
            _winLenght = GetWinLenght();
            _onGameFinish = onGameFinish;

            _playerX = new Player(gameField, Figure.CROSS);
            _playerO = new Player(gameField, Figure.NOUGHT);
            
            CreatePlayers(gameMode, gameField);
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

        public int GetWinLenght()
        {
            int maxRowLenght = Mathf.Max(_gameField.Grid.Rows, _gameField.Grid.Cols);

            if (maxRowLenght <= 3) {
                return 3;
            } 
            else if (maxRowLenght <= 8) {
                return 4; 
            }
            else {
                return 5;
            }
        }

        private bool CheckGameFinish(CellPosition position, IPlayer currentPlayer)
        {
            if (IsWin(position, currentPlayer, out RowType rowType))
            {
                List<Cell> winCells = _gameField.RowChecker.GetCellsOnRow(position, currentPlayer.Figure, rowType);

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
                    float randValue = Random.Range(-1f, 1f);

                    if (randValue > 0)
                    {
                        _playerX = new Player(gameField, Figure.CROSS);
                        _playerO = new SmartBot(gameField, Figure.NOUGHT);
                    }
                    else
                    {
                        _playerX = new SmartBot(gameField, Figure.CROSS);
                        _playerO = new Player(gameField, Figure.NOUGHT);
                    }
                    
                    break;
                
                case GameMode.BotVsBot:
                    _playerX = new SmartBot(gameField, Figure.CROSS);
                    _playerO = new SmartBot(gameField, Figure.NOUGHT);
                    break;
            }
        }

        private bool IsWin(CellPosition position, IPlayer currentPlayer, out RowType rowType)
        {
            return _gameField.RowChecker.GetLenght(position, currentPlayer.Figure, out rowType) == _winLenght;
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