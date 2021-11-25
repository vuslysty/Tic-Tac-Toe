using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using UnityEngine;

namespace Logic
{
    public class SmartBot : IBot
    {
        private readonly GameField _gameField;
        public Figure Figure { get; }

        public SmartBot(GameField gameField, Figure figure)
        {
            _gameField = gameField;
            Figure = figure;
        }

        public async Task<CellPosition> GetRightToMove()
        {
            var bestCell = GetBestCell();

            await Task.Delay(2000);

            bestCell.SetFigure(Figure);
            
            return _gameField.Grid.GetCellPosition(bestCell);
        }

        public void Cleanup()
        {
        }

        private Cell GetRandomCell()
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
            return randomCell;
        }

        private Cell GetBestCell()
        {
            Cell maxLenghtCell = null;
            int maxLenght = 0;

            foreach (Cell cell in _gameField.Grid)
            {
                CellPosition position = _gameField.Grid.GetCellPosition(cell);

                if (cell.IsEmpty())
                {
                    int lenght = _gameField.RowChecker.GetLenght(position, Figure);

                    if (lenght > maxLenght)
                    {
                        maxLenghtCell = cell;
                        maxLenght = lenght;
                    }
                }
            }

            if (maxLenght > 1)
            {
                return maxLenghtCell;
            }
            else
            {
                return GetRandomCell();
            }
        }
    }
}