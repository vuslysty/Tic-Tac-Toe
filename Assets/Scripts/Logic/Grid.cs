using System.Collections;

namespace Logic
{
    public class Grid : IEnumerator, IEnumerable
    {
        public Cell[,] Cells { get; set; }
        public int Rows { get; }
        public int Cols { get; }

        private CellPosition _currentEnumerablePosition = new CellPosition();

        public Grid(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        
            InitCells(rows, cols);
        }

        private void InitCells(int rows, int cols)
        {
            Cells = new Cell[rows, cols];

            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    Cells[row, col] = new Cell();
                }
            }
        }

        public Cell GetCell(CellPosition position)
        {
            return IsValidCell(position) ? Cells[position.Row, position.Col] : null;
        }

        public Cell GetCell(int row, int col)
        {
            CellPosition position = new CellPosition(row, col);

            return GetCell(position);
        }

        public CellPosition GetCellPosition(Cell cell)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (Cells[row, col] == cell)
                    {
                        return new CellPosition(row, col);
                    }
                }
            }

            return null;
        }

        public bool IsValidCell(CellPosition position)
        {
            return position.Row >= 0 && position.Row < Rows && 
                   position.Col >= 0 && position.Col < Cols;
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public bool MoveNext()
        {
            _currentEnumerablePosition.Col++;

            if (_currentEnumerablePosition.Col >= Cols)
            {
                _currentEnumerablePosition.Col = 0;
                _currentEnumerablePosition.Row++;
            }

            return IsValidCell(_currentEnumerablePosition);
        }

        public void Reset()
        {
            _currentEnumerablePosition.Col = -1;
            _currentEnumerablePosition.Row = 0;
        }

        public object Current => GetCell(_currentEnumerablePosition);
    }
}