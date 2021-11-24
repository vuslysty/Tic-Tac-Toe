using System;
using System.Collections.Generic;
using Enums;

namespace Logic
{
    public class RowChecker
    {
        private static readonly RowType[] RowTypes = 
        {
            RowType.Horizontal,
            RowType.Vertical,
            RowType.LeftTopToRightBottomDiagonal,
            RowType.LeftBottomToRightTopDiagonal
        };

        private readonly Grid _grid;

        public RowChecker(Grid grid)
        {
            _grid = grid;
        }

        public int GetLenght(int row, int col, Figure figure)
        {
            CellPosition position = new CellPosition(row, col);

            return GetLenght(position, figure);
        }
    
        public int GetLenght(CellPosition position, Figure figure)
        {
            return GetMaxLengthOnRows(position, figure, out RowType maxLenghtRow);
        }
    
        public int GetLenght(CellPosition position, Figure figure, out RowType maxLenghtRow)
        {
            return GetMaxLengthOnRows(position, figure, out maxLenghtRow);
        }

        public List<Cell> GetCellsOnRow(CellPosition position, Figure figure, RowType rowType)
        {
            List<Cell> cellsOnRow = new List<Cell>();
            Cell cell = _grid.GetCell(position);

            if (IsPossibleCellWithRightFigure(cell, figure))
            {
                cellsOnRow.Add(cell);
            
                switch (rowType)
                {
                    case RowType.Horizontal:
                        GetCellsOnDirection(position, figure, DirectionType.Left, cellsOnRow);
                        GetCellsOnDirection(position, figure, DirectionType.Right, cellsOnRow);
                        break;
                    case RowType.Vertical:
                        GetCellsOnDirection(position, figure, DirectionType.Top, cellsOnRow);
                        GetCellsOnDirection(position, figure, DirectionType.Bottom, cellsOnRow);
                        break;
                    case RowType.LeftTopToRightBottomDiagonal:
                        GetCellsOnDirection(position, figure, DirectionType.LeftTop, cellsOnRow);
                        GetCellsOnDirection(position, figure, DirectionType.RightBottom, cellsOnRow);
                        break;
                    case RowType.LeftBottomToRightTopDiagonal:
                        GetCellsOnDirection(position, figure, DirectionType.LeftBottom, cellsOnRow);
                        GetCellsOnDirection(position, figure, DirectionType.RightTop, cellsOnRow);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(rowType), rowType, null);
                }
            }

            return cellsOnRow;
        }

        private int GetMaxLengthOnRows(CellPosition position, Figure figure, out RowType maxLenghtRow)
        {
            maxLenghtRow = RowType.Unknown;
            int maxLenght = 0;

            foreach (RowType rowType in RowTypes)
            {
                int lenght = GetLengthOnRow(position, figure, rowType);

                if (lenght > maxLenght)
                {
                    maxLenght = lenght;
                    maxLenghtRow = rowType;
                }
            }

            return maxLenght;
        }

        private int GetLengthOnRow(CellPosition position, Figure figure, RowType rowType)
        {
            int length = 0;
            Cell cell = _grid.GetCell(position);

            if (IsPossibleCellWithRightFigure(cell, figure) || (cell != null && cell.GetFigure() == Figure.NONE))
            {
                length = 1;
            
                switch (rowType)
                {
                    case RowType.Horizontal:
                        length += GetFigureLengthOnDirection(position, figure, DirectionType.Left) +
                                  GetFigureLengthOnDirection(position, figure, DirectionType.Right);
                        break;
                    case RowType.Vertical:
                        length += GetFigureLengthOnDirection(position, figure, DirectionType.Top) +
                                  GetFigureLengthOnDirection(position, figure, DirectionType.Bottom);
                        break;
                    case RowType.LeftTopToRightBottomDiagonal:
                        length += GetFigureLengthOnDirection(position, figure, DirectionType.LeftTop) +
                                  GetFigureLengthOnDirection(position, figure, DirectionType.RightBottom);
                        break;
                    case RowType.LeftBottomToRightTopDiagonal:
                        length += GetFigureLengthOnDirection(position, figure, DirectionType.LeftBottom) +
                                  GetFigureLengthOnDirection(position, figure, DirectionType.RightTop);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(rowType), rowType, null);
                }
            }

            return length;
        }

        private int GetFigureLengthOnDirection(CellPosition position, Figure figure, DirectionType direction)
        {
            (int row, int col) tupleNextCellPosition = DirectionTool.GetNextPosition(position.Row, position.Col, direction);
            CellPosition nextCellPosition = new CellPosition(tupleNextCellPosition.row, tupleNextCellPosition.col);
            Cell nextCell = _grid.GetCell(nextCellPosition);

            if (IsPossibleCellWithRightFigure(nextCell, figure))
            {
                return 1 + GetFigureLengthOnDirection(nextCellPosition, figure, direction);
            }
        
            return 0;
        }
    
        private void GetCellsOnDirection(CellPosition position, Figure figure, DirectionType direction, List<Cell> cells)
        {
            (int row, int col) tupleNextCellPosition = DirectionTool.GetNextPosition(position.Row, position.Col, direction);
            CellPosition nextCellPosition = new CellPosition(tupleNextCellPosition.row, tupleNextCellPosition.col);
            Cell nextCell = _grid.GetCell(nextCellPosition);

            if (IsPossibleCellWithRightFigure(nextCell, figure))
            {
                cells.Add(nextCell);
                GetCellsOnDirection(nextCellPosition, figure, direction, cells);
            }
        }

        private static bool IsPossibleCellWithRightFigure(Cell cell, Figure figure)
        {
            return cell != null && cell.GetFigure() == figure;
        }
    }
}