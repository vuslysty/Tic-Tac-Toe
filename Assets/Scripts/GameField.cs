using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class GameField : MonoBehaviour
{
    public Action OnFigureSetEvent;
    
    private Grid _grid;

    public GridLayoutGroup GridLayout;
    public GameConfig GameConfig;

    public CellBehaviour CellPrefab;

    private List<CellBehaviour> _cellBehaviours;

    private void Awake()
    {
        _grid = new Grid(GameConfig.Rows, GameConfig.Cols);

        foreach (Cell cell in _grid)
        {
            cell.OnFigureSetEvent += OnFigureSet;
        }
        
        InitializeLayoutGrid();
        CreateVisualCells();
        SetClickableOn();
    }

    private void OnFigureSet()
    {
        OnFigureSetEvent?.Invoke();

        StartCoroutine(TurnOffClickableOnTime(3.0f));
    }

    private IEnumerator TurnOffClickableOnTime(float time)
    {
        SetClickableOff();
        yield return new WaitForSeconds(time);
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

    private void CreateVisualCells()
    {
        _cellBehaviours = new List<CellBehaviour>();

        RowChecker checker = new RowChecker(_grid);
        
        foreach (Cell cell in _grid)
        {
            CellBehaviour cellBehaviour = Instantiate(CellPrefab, GridLayout.transform);
            cellBehaviour.Construct(cell, _grid.GetCellPosition(cell), checker);

            _cellBehaviours.Add(cellBehaviour);
        }
    }

    private void InitializeLayoutGrid()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        float fieldWidth = rectTransform.rect.width;
        float fieldHeight = rectTransform.rect.height;

        float cellWidth = (fieldWidth - ((GameConfig.Cols + 1) * 10)) / GameConfig.Cols;
        float cellHeight = (fieldHeight - ((GameConfig.Rows + 1) * 10)) / GameConfig.Rows;

        GridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}