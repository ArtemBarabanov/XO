using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP_XO.Model;

namespace MVP_XO.AI
{
    class Player
    {
        public Cell MakeMove(Cell[,] field, int x, int y)
        {
            if (field[x, y].CellState == State.Empty)
            {
                field[x, y].CellState = State.Man;
                return field[x, y];
            }
            else
            {
                return null;
            }
        }
    }
}
