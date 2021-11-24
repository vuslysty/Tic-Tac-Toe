using System.Collections.Generic;
using Enums;

namespace Logic
{
    public static class DirectionTool
    {
        private static readonly Dictionary<DirectionType, (int, int)> DirectionsMap = new Dictionary<DirectionType, (int, int)>
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

        public static (int row, int col) GetNextPosition(int currentRow, int currentCol, DirectionType directionType)
        {
            (int row, int col) position = DirectionsMap[directionType];

            return (currentRow + position.row, currentCol + position.col);
        }
    }
}