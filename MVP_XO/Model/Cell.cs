using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.Model
{
    enum State
    {
        Empty,
        Man,
        Comp
    }

    class Cell
    {
        public State CellState { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y, State cellState)
        {
            X = x;
            Y = y;
            CellState = cellState;
        }
    }
}
