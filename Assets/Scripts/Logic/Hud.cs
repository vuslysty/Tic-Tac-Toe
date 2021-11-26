using System;
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
        public Color colorOfTie;

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
            string resultText;

            if (figure == Figure.NONE)
            {
                resultText = "Tie in the game";
            }
            else
            {
                string figureText = figure == Figure.CROSS ? "X" : "O";
                string playerName = isBot ? "Bot" : "Player";

                resultText = $"{playerName} {figureText} won!";
            }

            playerMoveOrResultText.color = GetColorOfFigure(figure);
            playerMoveOrResultText.text = resultText;
        }

        public void UpdateWinCondition(int winRowLength)
        {
            winConditionText.text = $"Put {winRowLength} in row to win";
        }

        private Color GetColorOfFigure(Figure figure)
        {
            switch (figure)
            {
                case Figure.NONE:
                    return colorOfTie;
                case Figure.CROSS:
                    return colorX;
                case Figure.NOUGHT:
                    return colorO;
                default:
                    throw new ArgumentOutOfRangeException(nameof(figure), figure, null);
            }
        }
    }
}