using Infrastructure.AssetManagement;
using Infrastructure.Services.StaticData;
using Logic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        
        private Transform _gameFieldRoot;

        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHud()
        {
            GameObject hud = Instantiate(AssetPath.HudPath);
            return hud;
        }

        public GameObject CreateGameField()
        {
            CreateGameFieldRoot();
            
            GameObject gameFieldObject = Instantiate(AssetPath.GameFieldPath, _gameFieldRoot);
            GameField gameField = gameFieldObject.GetComponent<GameField>();
            
            gameField.Construct(_staticData.GetGameConfig(), this);
            
            return gameFieldObject;
        }

        public CellBehaviour CreateCellBehaviour(Cell cell, GameField gameField, Transform parent)
        {
            CellBehaviour cellBehaviour = Instantiate(AssetPath.CellPath, parent).GetComponent<CellBehaviour>();
            
            cellBehaviour.Construct(cell, gameField);

            return cellBehaviour;
        }
        
        private void CreateGameFieldRoot()
        {
            if (_gameFieldRoot == null)
            {
                _gameFieldRoot = _assets.Instantiate(AssetPath.GameFieldRootPath).transform;
            }
        }

        private GameObject Instantiate(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            return gameObject;
        }
        
        private GameObject Instantiate(string prefabPath, Transform parent)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, parent);
            return gameObject;
        }
    }
}