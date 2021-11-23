using System;
using Enums;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private Grid _grid;
    private GridLayout _gridLayout;

    public GameField(GridLayout gridLayout)
    {
        _gridLayout = gridLayout;
    }

    public void Construct(GameConfig gameConfig)
    {
        _grid = new Grid(gameConfig.Rows, gameConfig.Cols);
    }

    private void Awake()
    {
        throw new NotImplementedException();
    }

    public void PutFigure(int row, int col, Figure figure)
    {
        Cell cell = _grid.GetCell(row, col);

        if (cell != null)
        {
            cell.SetFigure(figure);
        }
    }
}