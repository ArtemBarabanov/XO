using MVP_XO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.Interfaces
{
    interface IView: IMenu
    {       
        event Action<int, int> ClickEvent;

        void SetComp(int x, int y, Turn first);
        void SetHuman(int x, int y, Turn first);
        void HumanVictory((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst);
        void CompVictory((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst);
        void GameIsOver();
    }
}
