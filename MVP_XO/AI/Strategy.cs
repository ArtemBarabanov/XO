using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.AI
{
    abstract class Strategy
    {
        Random rand = new Random();
        public abstract Cell MakeMove(Cell[,] field);
        protected Cell RandomMove(Cell[,] field)
        {
            while (true)
            {
                int x = rand.Next(3);
                int y = rand.Next(3);
                if (field[x, y].CellState == State.Empty)
                {
                    field[x, y].CellState = State.Comp;
                    return field[x, y];
                }
            }
        }
        protected Cell CanManWin(Cell[,] temp)
        {
            for (int i = 0; i < 3; i++)
            {
                if (temp[i, 0].CellState == State.Man && temp[i, 1].CellState == State.Man && temp[i, 2].CellState == State.Empty) { return temp[i, 2]; }
                if (temp[i, 0].CellState == State.Man && temp[i, 1].CellState == State.Empty && temp[i, 2].CellState == State.Man) { return temp[i, 1]; }
                if (temp[i, 0].CellState == State.Empty && temp[i, 1].CellState == State.Man && temp[i, 2].CellState == State.Man) { return temp[i, 0]; }
                if (temp[0, i].CellState == State.Man && temp[1, i].CellState == State.Man && temp[2, i].CellState == State.Empty) { return temp[2, i]; }
                if (temp[0, i].CellState == State.Man && temp[1, i].CellState == State.Empty && temp[2, i].CellState == State.Man) { return temp[1, i]; }
                if (temp[0, i].CellState == State.Empty && temp[1, i].CellState == State.Man && temp[2, i].CellState == State.Man) { return temp[0, i]; }
                if (temp[0, 0].CellState == State.Man && temp[1, 1].CellState == State.Man && temp[2, 2].CellState == State.Empty) { return temp[2, 2]; }
                if (temp[0, 0].CellState == State.Man && temp[1, 1].CellState == State.Empty && temp[2, 2].CellState == State.Man) { return temp[1, 1]; }
                if (temp[0, 0].CellState == State.Empty && temp[1, 1].CellState == State.Man && temp[2, 2].CellState == State.Man) { return temp[0, 0]; }
                if (temp[0, 2].CellState == State.Man && temp[1, 1].CellState == State.Man && temp[2, 0].CellState == State.Empty) { return temp[2, 0]; }
                if (temp[0, 2].CellState == State.Man && temp[1, 1].CellState == State.Empty && temp[2, 0].CellState == State.Man) { return temp[1, 1]; }
                if (temp[0, 2].CellState == State.Empty && temp[1, 1].CellState == State.Man && temp[2, 0].CellState == State.Man) { return temp[0, 2]; }
            }
            return null;
        }
        protected Cell CanCompWin(Cell[,] temp)
        {
            for (int i = 0; i < 3; i++)
            {
                if (temp[i, 0].CellState == State.Comp && temp[i, 1].CellState == State.Comp && temp[i, 2].CellState == State.Empty) { return temp[i, 2]; }
                if (temp[i, 0].CellState == State.Comp && temp[i, 1].CellState == State.Empty && temp[i, 2].CellState == State.Comp) { return temp[i, 1]; }
                if (temp[i, 0].CellState == State.Empty && temp[i, 1].CellState == State.Comp && temp[i, 2].CellState == State.Comp) { return temp[i, 0]; }
                if (temp[0, i].CellState == State.Comp && temp[1, i].CellState == State.Comp && temp[2, i].CellState == State.Empty) { return temp[2, i]; }
                if (temp[0, i].CellState == State.Comp && temp[1, i].CellState == State.Empty && temp[2, i].CellState == State.Comp) { return temp[1, i]; }
                if (temp[0, i].CellState == State.Empty && temp[1, i].CellState == State.Comp && temp[2, i].CellState == State.Comp) { return temp[0, i]; }
                if (temp[0, 0].CellState == State.Comp && temp[1, 1].CellState == State.Comp && temp[2, 2].CellState == State.Empty) { return temp[2, 2]; }
                if (temp[0, 0].CellState == State.Comp && temp[1, 1].CellState == State.Empty && temp[2, 2].CellState == State.Comp) { return temp[1, 1]; }
                if (temp[0, 0].CellState == State.Empty && temp[1, 1].CellState == State.Comp && temp[2, 2].CellState == State.Comp) { return temp[0, 0]; }
                if (temp[0, 2].CellState == State.Comp && temp[1, 1].CellState == State.Comp && temp[2, 0].CellState == State.Empty) { return temp[2, 0]; }
                if (temp[0, 2].CellState == State.Comp && temp[1, 1].CellState == State.Empty && temp[2, 0].CellState == State.Comp) { return temp[1, 1]; }
                if (temp[0, 2].CellState == State.Empty && temp[1, 1].CellState == State.Comp && temp[2, 0].CellState == State.Comp) { return temp[0, 2]; }
            }
            return null;
        }
    }
}
