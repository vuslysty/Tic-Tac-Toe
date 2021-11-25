using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Random = UnityEngine.Random;

namespace Logic
{
    public class Bot : IBot
    {
        private readonly GameField _gameField;
        public Figure Figure { get; }

        public Bot(GameField gameField, Figure figure)
        {
            _gameField = gameField;
            Figure = figure;
        }

        public async Task<CellPosition> GetRightToMove()
        {
            List<Cell> emptyCells = new List<Cell>();
            
            foreach (Cell cell in _gameField.Grid)
            {
                if (cell.IsEmpty())
                {
                    emptyCells.Add(cell);
                }
            }

            int randomCellIndex = Random.Range(0, emptyCells.Count);
            Cell randomCell = emptyCells[randomCellIndex];

            await Task.Delay(2000);

            randomCell.SetFigure(Figure);
            
            return _gameField.Grid.GetCellPosition(randomCell);
        }

        public void Cleanup()
        {
        }
    }
}