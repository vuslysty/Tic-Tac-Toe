using System.Collections.Generic;

public class RowDirection
{
    private Dictionary<DirectionType, (int, int)> _directionsMap;

    public RowDirection()
    {
        _directionsMap = new Dictionary<DirectionType, (int, int)>
        {
            [DirectionType.Top] = (0, 1),
            [DirectionType.Bottom] = (0, -1),
            [DirectionType.Left] = (-1, 0),
            [DirectionType.Right] = (1, 0),
            [DirectionType.LeftTop] = (-1, 1),
            [DirectionType.LeftBottom] = (-1, -1),
            [DirectionType.RightTop] = (1, 1),
            [DirectionType.RightBottom] = (1, -1)
        };
    }

    public (int row, int col) GetNextPosition(int row, int col, DirectionType directionType)
    {
        (int row, int col) position = _directionsMap[directionType];

        return (row + position.row, col + position.col);
    }
    
    public CellPosition GetNextPosition(CellPosition cellPosition, DirectionType directionType)
    {
        (int row, int col) position = _directionsMap[directionType];

        return new CellPosition(cellPosition.Row + position.row, cellPosition.Col + position.col);
    }
}