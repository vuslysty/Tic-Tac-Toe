using Enums;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class Hud : MonoBehaviour
    {
        public TextMeshProUGUI playerMoveOrResultText;
        public TextMeshProUGUI winConditionText;

        public Color colorX;
        public Color colorO;

        public void UpdatePlayerMoveText(Figure figure, bool isBot)
        {
            string figureText = figure == Figure.CROSS ? "X" : "O";
            string playerName = isBot ? "Bot" : "Player";
            
            string text = $"{playerName} {figureText} is moving";

            playerMoveOrResultText.color = figure == Figure.CROSS ? colorX : colorO;
            playerMoveOrResultText.text = text;
        }
        
        public void UpdateResultText(Figure figure, bool isBot)
        {
            string figureText = figure == Figure.CROSS ? "X" : "O";
            string playerName = isBot ? "Bot" : "Player";
            
            string text = $"{playerName} {figureText} won!";

            playerMoveOrResultText.color = figure == Figure.CROSS ? colorX : colorO;
            playerMoveOrResultText.text = text;
        }

        public void UpdateWinCondition(int winRowLength)
        {
            winConditionText.text = $"Put {winRowLength} in row to win";
        }
    }
}