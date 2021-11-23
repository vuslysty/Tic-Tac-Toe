using System;
using System.Collections;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CellBehaviour : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnSelectEvent;
    public Action OnDeselectEvent;
    
    private const float ShowFigureTime = 0.2f;

    public Image spriteX;
    public Image spriteO;

    public TextMeshProUGUI number;

    private Cell _cell;
    private RowChecker _rowChecker;
    private CellPosition _position;

    public bool Clickable { get; set; }

    public void Construct(Cell cell, CellPosition position, RowChecker rowChecker)
    {
        _cell = cell;
        _cell.OnFigureSetEvent += OnFigureSet;

        _rowChecker = rowChecker;
        _position = position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Clickable)
        {
            _cell.SetFigure(Random.value - 0.5 > 0 ? Figure.CROSS : Figure.NOUGHT);
        }
    }

    private void OnFigureSet()
    {
        if (_cell.GetFigure() == Figure.CROSS)
        {
            StartCoroutine(ShowFigure(spriteX, ShowFigureTime));
        }
        else if (_cell.GetFigure() == Figure.NOUGHT)
        {
            StartCoroutine(ShowFigure(spriteO, ShowFigureTime));
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        number.text = _rowChecker.GetLenght(_position, Figure.CROSS).ToString();
        
        number.gameObject.SetActive(true);
        
        OnSelectEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        number.gameObject.SetActive(false);
        OnDeselectEvent?.Invoke();
    }
}