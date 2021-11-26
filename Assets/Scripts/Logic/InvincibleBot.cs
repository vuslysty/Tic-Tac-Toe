using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using UnityEngine;

namespace Logic
{
    public class InvincibleBot : IBot
    {
        private readonly GameField _gameField;
        private readonly int _winLenght;
        
        private readonly List<Cell> _myBest = new List<Cell>();
        private readonly List<Cell> _opponentBest = new List<Cell>();
        
        public Figure Figure { get; }

        public InvincibleBot(GameField gameField, Figure figure, int winLenght)
        {
            _gameField = gameField;
            _winLenght = winLenght;
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

        private Cell GetBestCell()
        {
            FillBestCellArrays();
            SortBestCellArrays();

            if (_myBest.Count == 0 && _opponentBest.Count > 0)
                return _opponentBest.Last();
            if (_myBest.Count > 0 && _opponentBest.Count == 0)
                return _myBest.Last();
            if (_myBest.Count == 0 && _opponentBest.Count == 0)
                return GetRandomCell();

            Cell bestOpponentCell = _opponentBest.Last();
            Cell bestMyCell = _myBest.Last();
            
            CellPosition myBestCellPosition = _gameField.Grid.GetCellPosition(bestMyCell);
            CellPosition opponentBestCellPosition = _gameField.Grid.GetCellPosition(bestOpponentCell);
            
            int myLenght = _gameField.RealChecker.GetLenght(myBestCellPosition, Figure);
            int opponentLenght = _gameField.RealChecker.GetLenght(opponentBestCellPosition, OpponentFigure());

            return myLenght > opponentLenght ? bestMyCell : bestOpponentCell;
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

        private void FillBestCellArrays()
        {
            int opponentMaxLenght = 0;
            int myMaxLenght = 0;

            _myBest.Clear();
            _opponentBest.Clear();

            foreach (Cell cell in _gameField.Grid)
            {
                CellPosition position = _gameField.Grid.GetCellPosition(cell);

                if (cell.IsEmpty())
                {
                    RowType rowType;
                    
                    int opponentRealLenght = _gameField.RealChecker.GetLenght(position, OpponentFigure(), out rowType);
                    int opponentPossibleLenght = _gameField.PossibleChecker.GetLengthOnRow(position, OpponentFigure(), rowType);

                    if (opponentRealLenght >= opponentMaxLenght && opponentPossibleLenght >= _winLenght)
                    {
                        _opponentBest.Add(cell);
                        opponentMaxLenght = opponentRealLenght;
                    }

                    int myRealLenght = _gameField.RealChecker.GetLenght(position, Figure, out rowType);
                    int myPossibleLenght = _gameField.PossibleChecker.GetLengthOnRow(position, Figure, rowType);

                    if (myRealLenght >= myMaxLenght && myPossibleLenght >= _winLenght)
                    {
                        _myBest.Add(cell);
                        myMaxLenght = myRealLenght;
                    }
                }
            }
        }

        private void SortBestCellArrays()
        {
            if (Figure == Figure.CROSS)
            {
                _myBest.Sort(XCellComparison);
                _opponentBest.Sort(OCellComparison);
            }
            else
            {
                _myBest.Sort(OCellComparison);
                _opponentBest.Sort(XCellComparison);
            }
        }

        private int XCellComparison(Cell x, Cell y)
        {
            return CellComparison(x, y, Figure.CROSS);
        }

        private int OCellComparison(Cell x, Cell y)
        {
            return CellComparison(x, y, Figure.NOUGHT);
        }

        private int CellComparison(Cell x, Cell y, Figure figure)
        {
            CellPosition xPosition = _gameField.Grid.GetCellPosition(x);
            CellPosition yPosition = _gameField.Grid.GetCellPosition(y);

            RowType xRowType;
            RowType yRowType;

            int xLenght = _gameField.RealChecker.GetLenght(xPosition, figure, out xRowType);
            int yLenght = _gameField.RealChecker.GetLenght(yPosition, figure, out yRowType);

            if (xLenght > yLenght)
            {
                return 1;
            }

            if (xLenght < yLenght)
            {
                return -1;
            }
            
            //--------------------------------------------------------------------------------
            
            int xSides = _gameField.RealChecker.GetSidesCountOnRow(xPosition, figure, xRowType);
            int ySides = _gameField.RealChecker.GetSidesCountOnRow(yPosition, figure, yRowType);

            if (xSides > ySides)
            {
                return 1;
            }

            if (xSides < ySides)
            {
                return -1;
            }

            //--------------------------------------------------------------------------------

            
            int centerRow = _gameField.Grid.Rows / 2;
            int centerCol = _gameField.Grid.Cols / 2;

            int xDistanceToCenter = (int)(Mathf.Pow(xPosition.Row - centerRow, 2) + Mathf.Pow(xPosition.Col - centerCol, 2));
            int yDistanceToCenter = (int)(Mathf.Pow(yPosition.Row - centerRow, 2) + Mathf.Pow(yPosition.Col - centerCol, 2));

            if (xDistanceToCenter < yDistanceToCenter)
            {
                return 1;
            }

            if (xDistanceToCenter > yDistanceToCenter)
            {
                return -1;
            }

            return 0;
        }

        private Figure OpponentFigure()
        {
            return Figure == Figure.CROSS ? Figure.NOUGHT : Figure.CROSS;
        }
    }
}