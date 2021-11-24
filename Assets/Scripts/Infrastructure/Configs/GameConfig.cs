using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        public int Rows = 8;
        public int Cols = 8;
    }
}