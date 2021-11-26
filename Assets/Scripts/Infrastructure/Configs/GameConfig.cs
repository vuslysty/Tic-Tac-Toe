using Enums;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Configs
{
    public class GameConfig : IGameConfig
    {
        public int Rows { get; set; }
        public int Cols { get; set; }

        public GameMode GameMode { get; set; }
        public BotType BotType { get; set; }

        public int GetWinLenght()
        {
            int maxRowLenght = Mathf.Max(Rows, Cols);

            if (maxRowLenght <= 3) {
                return 3;
            }
            if (maxRowLenght <= 8) {
                return 4; 
            }
            return 5;
        }
    }
}