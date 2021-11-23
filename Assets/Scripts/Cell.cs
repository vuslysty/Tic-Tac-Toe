using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Action OnFigureSetEvent;

    private Image _image;
    
    private Figure _figure;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image.type = Image.Type.Filled;
        
        
        _figure = Figure.NONE;
    }

    public bool IsEmpty()
    {
        return _figure == Figure.NONE;
    }

    public Figure GetFigure()
    {
        return _figure;
    }

    public void SetFigure(Figure figure)
    {
        if (IsEmpty())
        {
            
        }
    }
}