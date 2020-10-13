using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.AI
{
    class Computer
    {
        public Strategy CurrentStrategy { get; private set; }
        Cell[,] field;
        
        public Computer(Strategy Strategy, Cell[,] field)
        {
            CurrentStrategy = Strategy;
            this.field = field;
        }

        public Cell MakeMove()
        {
            return CurrentStrategy.MakeMove(field);
        }
    }
}
