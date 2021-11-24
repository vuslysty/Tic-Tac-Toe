using System;
using System.Collections.Generic;
using Enums;
using Infrastructure.Configs;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class GameField : MonoBehaviour
    {
        public Action<CellPosition> onFigureSetEvent;

        public GridLayoutGroup gridLayout;

        private IGameFactory _gameFactory;
        
        private Grid _grid;
        private RowChecker _rowChecker;
        private GameConfig _gameConfig;

        private List<CellBehaviour> _cellBehaviours;

        public void Construct(GameConfig config, IGameFactory gameFactory)
        {
            _gameConfig = config;
            _gameFactory = gameFactory;

            _grid = new Grid(config.Rows, config.Cols);
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
            onFigureSetEvent?.Invoke(_grid.GetCellPosition(cell));
        }

        private void InitializeLayoutGrid()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
        
            float fieldWidth = rectTransform.rect.width;
            float fieldHeight = rectTransform.rect.height;

            float horizontalSpacesLenght = (_gameConfig.Cols + 1) * gridLayout.spacing.x;
            float verticalSpacesLenght = (_gameConfig.Rows + 1) * gridLayout.spacing.y;
        
            float cellWidth = (fieldWidth - horizontalSpacesLenght) / _gameConfig.Cols;
            float cellHeight = (fieldHeight - verticalSpacesLenght) / _gameConfig.Rows;

            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
        }

        private void CreateVisualCells()
        {
            _cellBehaviours = new List<CellBehaviour>();

            foreach (Cell cell in _grid)
            {
                CellBehaviour cellBehaviour = _gameFactory.CreateCellBehaviour(cell, _grid.GetCellPosition(cell),
                    _rowChecker, gridLayout.transform);

                _cellBehaviours.Add(cellBehaviour);
            }
        }
    }
}