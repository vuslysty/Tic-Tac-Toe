using System;
using Enums;

namespace Logic
{
    public class Cell
    {
        public Action<Cell> OnFigureSetEvent;

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
                OnFigureSetEvent?.Invoke(this);

                result = true;
            }

            return result;
        }
    }
}