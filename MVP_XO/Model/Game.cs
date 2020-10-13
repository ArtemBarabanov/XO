using MVP_XO.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVP_XO.Model
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    public enum Turn
    {
        None,
        Human,
        Computer
    }

    class Game
    {
        Player Man; // Игрок
        Computer Comp; //Компьютер
        Turn turn;

        Cell[,] Field = new Cell[3, 3];
        Difficulty Difficulty;
        Turn First;

        int StepCount;
        bool isVictorious;
        bool isNobody;

        public event Action<int, int, Turn> ManMoveEvent;
        public event Action<int, int, Turn> CompMoveEvent;
        public event Action<(int,int), (int,int), (int, int), Turn> CompVictoryEvent;
        public event Action<(int, int), (int, int), (int, int), Turn> HumanVictoryEvent;
        public event Action NobodyVictoryEvent;

        /// <summary>
        /// Настройка начальных параметров игры
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="first"></param>
        public void SetGameOptions(Difficulty difficulty, Turn first) 
        {
            Difficulty = difficulty;
            First = first;
            CreateField();
            AddPlayers();
            isNobody = false;
            isVictorious = false;
            StepCount = 0;
            if (first == Turn.Computer)
            {
                CompMove();
            }
            else 
            {
                turn = Turn.Human;
            }
        }

        /// <summary>
        /// Создание экземпляра оппонента со стратегией, соответствующей уровню сложности
        /// </summary>
        private void AddPlayers()
        {
            if (Difficulty == Difficulty.Easy)
            {
                Comp = new Computer(new SimpleStrategy(), Field);
            }
            else if (Difficulty == Difficulty.Normal)
            {
                Comp = new Computer(new NormalStrategy(), Field);
            }
            else if (Difficulty == Difficulty.Hard)
            {
                Comp = new Computer(new CleverStrategy(), Field);
            }
            Man = new Player();
        }

        private void CreateField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Field[i, j] = new Cell(i, j, State.Empty);
                }
            }
        }

        /// <summary>
        /// Ход человека
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ManMove(int x, int y)
        {
            Cell cell = Man.MakeMove(Field, x, y);
            if (cell != null)
            {
                ManMoveEvent(cell.X, cell.Y, First);
                turn = Turn.Computer;
                StepCount++;
            }
        }

        /// <summary>
        /// Ход компьютера
        /// </summary>
        private void CompMove()
        {
            Cell cell = Comp.MakeMove();
            if (cell != null)
            {
                CompMoveEvent(cell.X, cell.Y, First);
                turn = Turn.Human;
                StepCount++;
            }
        }

        /// <summary>
        /// Один игровой цикл
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Move(int x, int y) 
        {
            if (turn == Turn.Human && !isVictorious && !isNobody)
            {
                ManMove(x, y);
                ScanField();
            }
            if (turn == Turn.Computer && !isVictorious && !isNobody)
            {
                CompMove();
                ScanField();
            }
        }

        #region Проверка поля
        private void ScanField()
        {
            CompWin();
            HumanWin();
            NoOneWin();
        }

        //Ничья
        private void NoOneWin()
        {
            if (StepCount == 9 && !isVictorious)
            {
                NobodyVictoryEvent();
                isNobody = true;
            }
        }

        //Выиграл ли компьютер
        private void CompWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Field[i, 0].CellState == State.Comp && Field[i, 1].CellState == State.Comp && Field[i, 2].CellState == State.Comp)
                {
                    CompVictoryEvent((i, 0), (i, 1), (i, 2), First);
                    isVictorious = true;
                    return;
                }
                else if ((Field[0, i].CellState == State.Comp && Field[1, i].CellState == State.Comp && Field[2, i].CellState == State.Comp))
                {
                    CompVictoryEvent((0, i), (1, i), (2, i), First);
                    isVictorious = true;
                    return;
                }
            }

            if (Field[0, 0].CellState == State.Comp && Field[1, 1].CellState == State.Comp && Field[2, 2].CellState == State.Comp)
            {
                CompVictoryEvent((0, 0), (1, 1), (2, 2), First);
                isVictorious = true;
                return;
            }

            if (Field[0, 2].CellState == State.Comp && Field[1, 1].CellState == State.Comp && Field[2, 0].CellState == State.Comp)
            {
                CompVictoryEvent((0, 2), (1, 1), (2, 0), First);
                isVictorious = true;
                return;
            }
        }

        //Выиграл ли человек
        private void HumanWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Field[i, 0].CellState == State.Man && Field[i, 1].CellState == State.Man && Field[i, 2].CellState == State.Man)
                {
                    HumanVictoryEvent((i, 0), (i, 1), (i, 2), First);
                    isVictorious = true;
                    return;
                }
                else if (Field[0, i].CellState == State.Man && Field[1, i].CellState == State.Man && Field[2, i].CellState == State.Man)
                {
                    HumanVictoryEvent((0, i), (1, i), (2, i), First);
                    isVictorious = true;
                    return;
                }
            }

            if (Field[0, 0].CellState == State.Man && Field[1, 1].CellState == State.Man && Field[2, 2].CellState == State.Man)
            {
                HumanVictoryEvent((0, 0), (1, 1), (2, 2), First);
                isVictorious = true;
                return;
            }

            if (Field[0, 2].CellState == State.Man && Field[1, 1].CellState == State.Man && Field[2, 0].CellState == State.Man)
            {
                HumanVictoryEvent((0, 2), (1, 1), (2, 0), First);
                isVictorious = true;
                return;
            }
        }
        #endregion
    }
}
