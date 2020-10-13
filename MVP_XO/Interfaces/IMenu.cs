using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_XO.Interfaces
{
    interface IMenu
    {
        event EventHandler LowDifficultyClick;
        event EventHandler NormalDifficultyClick;
        event EventHandler HighDifficultyClick;
        event EventHandler StartClick;
        event EventHandler ManFirstEvent;
        event EventHandler CompFirstEvent;
    }
}
