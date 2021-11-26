using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logic
{
    public class CellBehaviour : MonoBehaviour, IPointerDownHandler
    {
        private const float ShowFigureTime = 0.2f;

        public Image spriteX;
        public Image spriteO;

        private Cell _cell;
        private GameField _gameField;

        public Cell Cell => _cell;

        public bool Clickable { get; set; }

        public void Construct(Cell cell, GameField gameField)
        {
            _cell = cell;
            _gameField = gameField;
            
            _cell.OnFigureSetEvent += OnFigureSet;
        }

        public void OnDestroy()
        {
            _cell.OnFigureSetEvent -= OnFigureSet;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Clickable)
            {
                _cell.SetFigure(_gameField.FigureOnClick);
            }
        }

        private void OnFigureSet(Cell cell)
        {
            Figure currentFigure = _cell.GetFigure();

            switch (currentFigure)
            {
                case Figure.CROSS:
                    StartCoroutine(ShowFigure(spriteX, ShowFigureTime));
                    break;
                case Figure.NOUGHT:
                    StartCoroutine(ShowFigure(spriteO, ShowFigureTime));
                    break;
            }
        }

        private IEnumerator ShowFigure(Image image, float time)
        {
            float elapsed = 0;

            while (elapsed < time)
            {
                image.fillAmount = Mathf.Lerp(0, 1, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }

            image.fillAmount = 1;
        }

        public void PlayAnimation()
        {
            StartCoroutine(PlayAnimationRoutine(0.5f));
        }

        private IEnumerator PlayAnimationRoutine(float fullCycleTime)
        {
            Transform figureImageTransform = _cell.GetFigure() == Figure.CROSS ? spriteX.transform : spriteO.transform;
            
            while (true)
            {
                yield return ChangeScaleRoutine(figureImageTransform, 0.8f, fullCycleTime / 2);
                yield return ChangeScaleRoutine(figureImageTransform, 1, fullCycleTime / 2);
            }
        }

        private IEnumerator ChangeScaleRoutine(Transform transform, float to, float time)
        {
            Vector3 startScale = transform.localScale;
            Vector3 endScale = Vector3.one * to;
            float elapsed = 0;

            while (elapsed < time)
            {
                transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endScale;
        }
    }
}