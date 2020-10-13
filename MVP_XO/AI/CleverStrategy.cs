using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.AI
{
    class CleverStrategy : Strategy
    {
        Random rand = new Random();

        public override Cell MakeMove(Cell[,] field)
        {
            Cell canManWin = CanManWin(field);
            Cell canCompWin = CanCompWin(field);
            int x = 0;
            int y = 0;

            if (canCompWin != null)
            {
                x = canCompWin.X;
                y = canCompWin.Y;
                field[x, y].CellState = State.Comp;
                return canCompWin;
            }
            else if (canManWin != null)
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
