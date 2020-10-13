using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.AI
{
    class SimpleStrategy : Strategy
    {
        public override Cell MakeMove(Cell[,] field) 
        {
            return RandomMove(field);
        }
    }
}
