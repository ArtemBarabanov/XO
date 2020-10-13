using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVP_XO.Interfaces;
using MVP_XO.Model;

namespace MVP_XO
{
    public partial class Form1 : Form, IView
    {
        MyPictureBox[,] pictureField;

        public Form1()
        {
            InitializeComponent();
            background = new PictureBox() { Width = 500, Height = 700, Location = new Point(0, 0), SizeMode = PictureBoxSizeMode.StretchImage, Image = Properties.Resources.background };
            close = new PictureBox() { Width = 40, Height = 40, Location = new Point(455, 10), BackColor = Color.Transparent, Image = Properties.Resources.close };
            minimize = new PictureBox() { Width = 40, Height = 40, Location = new Point(410, 10), BackColor = Color.Transparent, Image = Properties.Resources.minimize };
            direction = new PictureBox() { Width = 40, Height = 40, Location = new Point(430, 550), BackColor = Color.Transparent, Image = Properties.Resources.direction1 };
            Controls.Add(background);
            LoadImages();
            LoadMotionFrames();
            start.Enabled = false;
        }

        private void CreateField()
        {
            field = new PictureBox() { Width = 240, Height = 240, Location = new Point(100, 150), BackColor = Color.Transparent, SizeMode = PictureBoxSizeMode.StretchImage };
            background.Controls.Add(field);
            pictureField = new MyPictureBox[3, 3];

            int X = 0;
            int Y = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pictureField[i, j] = new MyPictureBox(i, j);
                    pictureField[i, j].Size = new Size(70, 70);
                    pictureField[i, j].Location = new Point(X, Y);
                    pictureField[i, j].Click += MyPictureBox_Click;
                    pictureField[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    field.Controls.Add(pictureField[i, j]);
                    X += 85;
                }
                X = 0;
                Y += 85;
            }
        }

        private void BlockField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pictureField[i, j].Enabled = false;
                }
            }
        }

        private void UnblockField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pictureField[i, j].Enabled = true;
                }
            }
        }

        /// <summary>
        /// Клик по полю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPictureBox_Click(object sender, EventArgs e)
        {
            MyPictureBox btn = sender as MyPictureBox;
            ClickEvent(btn.X, btn.Y);
        }

        #region Реализация интерфейса
        public event Action<int, int> ClickEvent;
        public event EventHandler LowDifficultyClick;
        public event EventHandler NormalDifficultyClick;
        public event EventHandler HighDifficultyClick;
        public event EventHandler StartClick;
        public event EventHandler ManFirstEvent;
        public event EventHandler CompFirstEvent;

        /// <summary>
        /// Отрисовка хода компьютера
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="first"></param>
        public void SetComp(int x, int y, Turn first)
        {
            BlockField();
            CompDrawAsync(pictureField[x, y], first);
            UnblockField();
        }

        /// <summary>
        /// Отрисовка хода человека
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="first"></param>
        public void SetHuman(int x, int y, Turn first)
        {
            ManDrawAsync(pictureField[x, y], first);
        }

        /// <summary>
        /// Отрисовка конца игры
        /// </summary>
        public void GameIsOver()
        {
            InvokePanelNobodyWin();
            background.Controls.Add(again);
        }

        private async void ManDrawAsync(object obj, Turn first)
        {
            MyPictureBox clickedPic = obj as MyPictureBox;

            if (first == Turn.Human)
            {
                for (int i = 0; i < 12; i++)
                {
                    clickedPic.Image = imageCross[i];
                    await Task.Delay(50);
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    clickedPic.Image = imageCircle[i];
                    await Task.Delay(50);
                }
            }
        }

        private async void CompDrawAsync(object obj, Turn first)
        {
            MyPictureBox compClicked = obj as MyPictureBox;

            await Task.Delay(1000);
            if (first == Turn.Human)
            {
                for (int i = 0; i < 12; i++)
                {
                    Invoke(new Action(() => compClicked.Image = imageCircle[i]));
                    await Task.Delay(50);
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    Invoke(new Action(() => compClicked.Image = imageCross[i]));
                    await Task.Delay(50);
                }
            }
        }

        /// <summary>
        /// Отрисовка окна победы для человека
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="whoIsFirst"></param>
        public void HumanVictory((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                if (whoIsFirst == Turn.Human)
                {
                    pictureField[first.Item1, first.Item2].Image = Properties.Resources.crosswin;
                    await Task.Delay(300);
                    pictureField[second.Item1, second.Item2].Image = Properties.Resources.crosswin;
                    await Task.Delay(300);
                    pictureField[third.Item1, third.Item2].Image = Properties.Resources.crosswin;
                }
                else
                {
                    pictureField[first.Item1, first.Item2].Image = Properties.Resources.circlewin;
                    await Task.Delay(300);
                    pictureField[second.Item1, second.Item2].Image = Properties.Resources.circlewin;
                    await Task.Delay(300);
                    pictureField[third.Item1, third.Item2].Image = Properties.Resources.circlewin;
                }
            });
            InvokePanelManWin();
            background.Controls.Add(again);
        }

        /// <summary>
        /// Отрисовка окна победы для компьютера
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="whoIsFirst"></param>
        public void CompVictory((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                if (whoIsFirst == Turn.Computer)
                {
                    pictureField[first.Item1, first.Item2].Image = new Bitmap(Properties.Resources.crosswin);
                    await Task.Delay(300);
                    pictureField[second.Item1, second.Item2].Image = new Bitmap(Properties.Resources.crosswin);
                    await Task.Delay(300);
                    pictureField[third.Item1, third.Item2].Image = new Bitmap(Properties.Resources.crosswin);
                }
                else
                {
                    pictureField[first.Item1, first.Item2].Image = new Bitmap(Properties.Resources.circlewin);
                    await Task.Delay(300);
                    pictureField[second.Item1, second.Item2].Image = new Bitmap(Properties.Resources.circlewin);
                    await Task.Delay(300);
                    pictureField[third.Item1, third.Item2].Image = new Bitmap(Properties.Resources.circlewin);
                }
            });
            InvokePanelCompWin();
            background.Controls.Add(again);
        }
        #endregion

        /// <summary>
        /// Клик по кнопке Старт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            easy.Dispose();
            normal.Dispose();
            hard.Dispose();
            start.Dispose();
            main_screen.Dispose();

            PrepareField();
        }

        #region Выбор уровня сложности
        private void Hard_Click(object sender, EventArgs e)
        {
            if (!isHard)
            {
                HighDifficultyClick(this, EventArgs.Empty);
                isHard = true;
                isNormal = false;
                isEasy = false;
                hard.Image = new Bitmap(Properties.Resources.hard_pressed);
                normal.Image = new Bitmap(Properties.Resources.normal);
                easy.Image = new Bitmap(Properties.Resources.easy);

                start.Image = new Bitmap(Properties.Resources.start);
                start.Enabled = true;
            };
        }

        private void Normal_Click(object sender, EventArgs e)
        {
            if (!isNormal)
            {
                NormalDifficultyClick(this, EventArgs.Empty);
                isHard = false;
                isNormal = true;
                isEasy = false;
                hard.Image = new Bitmap(Properties.Resources.hard);
                normal.Image = new Bitmap(Properties.Resources.normal_pressed);
                easy.Image = new Bitmap(Properties.Resources.easy);

                start.Image = new Bitmap(Properties.Resources.start);
                start.Enabled = true;
            };
        }

        private void Easy_Click(object sender, EventArgs e)
        {
            if (!isEasy)
            {
                LowDifficultyClick(this, EventArgs.Empty);
                isHard = false;
                isNormal = false;
                isEasy = true;
                hard.Image = new Bitmap(Properties.Resources.hard);
                normal.Image = new Bitmap(Properties.Resources.normal);
                easy.Image = new Bitmap(Properties.Resources.easy_pressed);

                start.Image = new Bitmap(Properties.Resources.start);
                start.Enabled = true;
            };
        }
        #endregion
    }
}
