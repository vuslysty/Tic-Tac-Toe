using Logic;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory : IService
    {
        GameObject CreateHud();
        GameObject CreateGameField();
        CellBehaviour CreateCellBehaviour(Cell cell, GameField gameField, Transform parent);
    }
}