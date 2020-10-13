using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.AI
{
    class NormalStrategy : Strategy
    {
        Random rand = new Random();

        public override Cell MakeMove(Cell[,] field)
        {
            Cell canManWin = CanManWin(field);
            int x = 0;
            int y = 0;

            if (canManWin != null)
            {
                x = canManWin.X;
                y = canManWin.Y;
                field[x, y].CellState = State.Comp;
                return canManWin;
            }
            else
            {
                return RandomMove(field);
            } 
        }
    }
}
