using System;
using System.Collections.Generic;
using Configs;
using Enums;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class GameField : MonoBehaviour
{
    public Action<CellPosition> OnFigureSetEvent;
    
    private Grid _grid;
    private RowChecker _rowChecker;

    public GridLayoutGroup GridLayout;
    public GameConfig GameConfig;

    public CellBehaviour CellPrefab;

    private List<CellBehaviour> _cellBehaviours;

    public void Construct(Grid grid, GameConfig config)
    {
        _grid = grid;
        GameConfig = config;
        
        _rowChecker = new RowChecker(_grid);
        
        foreach (Cell cell in _grid)
        {
            cell.OnFigureSetEvent += OnFigureSet;
        }
        
        InitializeLayoutGrid();
        CreateVisualCells();
        SetClickableOn();
    }
    
    private void Awake()
    {
        _grid = new Grid(GameConfig.Rows, GameConfig.Cols);

        _rowChecker = new RowChecker(_grid);

        foreach (Cell cell in _grid)
        {
            cell.OnFigureSetEvent += OnFigureSet;
        }
        
        InitializeLayoutGrid();
        CreateVisualCells();
        SetClickableOn();
    }

    public void PutFigure(int row, int col, Figure figure)
    {
        Cell cell = _grid.GetCell(row, col);

        if (cell != null)
        {
            cell.SetFigure(figure);
        }
    }

    public void SetClickableOn()
    {
        foreach (CellBehaviour cellBehaviour in _cellBehaviours)
        {
            cellBehaviour.Clickable = true;
        }
    }

    public void SetClickableOff()
    {
        foreach (CellBehaviour cellBehaviour in _cellBehaviours)
        {
            cellBehaviour.Clickable = false;
        }
    }

    private void OnFigureSet(Cell cell)
    {
        OnFigureSetEvent?.Invoke(_grid.GetCellPosition(cell));
    }

    private void InitializeLayoutGrid()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        float fieldWidth = rectTransform.rect.width;
        float fieldHeight = rectTransform.rect.height;

        float horizontalSpacesLenght = (GameConfig.Cols + 1) * GridLayout.spacing.x;
        float verticalSpacesLenght = (GameConfig.Rows + 1) * GridLayout.spacing.y;
        
        float cellWidth = (fieldWidth - horizontalSpacesLenght) / GameConfig.Cols;
        float cellHeight = (fieldHeight - verticalSpacesLenght) / GameConfig.Rows;

        GridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }

    private void CreateVisualCells()
    {
        _cellBehaviours = new List<CellBehaviour>();

        foreach (Cell cell in _grid)
        {
            CellBehaviour cellBehaviour = Instantiate(CellPrefab, GridLayout.transform);
            cellBehaviour.Construct(cell, _grid.GetCellPosition(cell), _rowChecker);

            _cellBehaviours.Add(cellBehaviour);
        }
    }
}