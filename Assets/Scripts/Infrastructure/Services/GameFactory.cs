using System;
using Enums;
using Infrastructure.AssetManagement;
using Infrastructure.Configs;
using Infrastructure.Services.StaticData;
using Logic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IGameConfig _gameConfig;

        private Transform _gameFieldRoot;

        public GameFactory(IAssets assets, IStaticDataService staticData, IGameConfig gameConfig)
        {
            _assets = assets;
            _staticData = staticData;
            _gameConfig = gameConfig;
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
            
            gameField.Construct(_gameConfig, this);
            
            return gameFieldObject;
        }

        public CellBehaviour CreateCellBehaviour(Cell cell, GameField gameField, Transform parent)
        {
            CellBehaviour cellBehaviour = Instantiate(AssetPath.CellPath, parent).GetComponent<CellBehaviour>();
            
            cellBehaviour.Construct(cell, gameField);

            return cellBehaviour;
        }

        public IBot CreateBot(BotType botType, GameField gameField, Figure figure)
        {
            IBot bot = null;
            
            switch (botType)
            {
                case BotType.Junior:
                    bot = new JuniorBot(gameField, figure, _gameConfig.GetWinLenght());
                    break;
                case BotType.Master:
                    bot = new MasterBot(gameField, figure, _gameConfig.GetWinLenght());
                    break;
                case BotType.Invincible:
                    bot = new InvincibleBot(gameField, figure, _gameConfig.GetWinLenght());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(botType), botType, null);
            }

            return bot;
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

    public enum BotType
    {
        Junior,
        Master,
        Invincible
    }
}