using System.Threading.Tasks;
using Enums;

namespace Logic
{
    class Player : IPlayer
    {
        public Figure Figure { get; }

        private readonly GameField _gameField;

        private bool _inMovingProgress;
        private CellPosition _position;

        public Player(GameField gameField, Figure figure)
        {
            _gameField = gameField;
            Figure = figure;
        }
        
        public async Task<CellPosition> GetRightToMove()
        {
            _gameField.onFigureSetEvent += OnFigureSet;
            _inMovingProgress = true;
            _gameField.SetClickableOn(Figure);

            while (_inMovingProgress)
            {
                await Task.Yield();
            }

            return _position;
        }

        public void Cleanup()
        {
            _gameField.onFigureSetEvent -= OnFigureSet;
        }

        private void OnFigureSet(CellPosition position)
        {
            Cleanup();
            
            _position = position;
            _gameField.SetClickableOff();

            _inMovingProgress = false;
        }
    }
}