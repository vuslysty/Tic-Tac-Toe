public class Grid
{
    public Cell[,] Cells { get; }
    public int Rows { get; }
    public int Cols { get; }

    public Grid(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Cells = new Cell[rows, cols];
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

    public bool IsValidCell(CellPosition position)
    {
        return position.Row >= 0 && position.Row < Rows && 
               position.Col >= 0 && position.Col < Cols;
    }
}