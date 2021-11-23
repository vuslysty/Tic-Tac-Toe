using System;
using Enums;

public class Cell
{
    public Action OnFigureSetEvent;

    private Figure _figure;

    public Cell()
    {
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

    public bool SetFigure(Figure figure)
    { 
        bool result = false;
        
        if (IsEmpty())
        {
            _figure = figure;
            OnFigureSetEvent?.Invoke();

            result = true;
        }

        return result;
    }
}