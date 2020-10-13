using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP_XO
{
    //Графические элементы

    partial class Form1
    {
        Image[] imageCross = new Image[12]; //Кадры крестика
        Image[] imageCircle = new Image[12]; //Кадры нолика
        Image[] imageField = new Image[16]; //Кадры поля
        Image[] imageHorizontalWin = new Image[6];
        Image[] imageCompWin = new Image[26]; //Кадры победы компьютера
        Image[] imageManWin = new Image[24]; //Кадры победы человека
        Image[] imageNobodyWin = new Image[20]; //Кадры ничьи
        Image[] imageWhoFirst = new Image[14]; //Кадры разыгрыша хода

        PictureBox background; //фон формы
        PictureBox field; //сетка поля
        PictureBox close; //крестик закрытия
        PictureBox minimize; //иконка сворачивания
        PictureBox win; //
        PictureBox easy; //кнопка Легкая сложность
        PictureBox normal; //кнопка Нормальная сложность
        PictureBox hard; //кнопка Тяжелая сложность
        PictureBox start; //кнопка начала игры
        PictureBox whoFirst;
        PictureBox main_screen;
        PictureBox panelup;
        PictureBox paneldown;
        PictureBox direction;
        PictureBox again; //кнопка повторить игру

        private void LoadImages()
        {
            FormBorderStyle = FormBorderStyle.None;

            easy = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 400), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.easy };
            normal = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 480), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.normal };
            hard = new PictureBox() { Width = 200, Height = 80, Location = new Point(80, 560), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.hard };
            start = new PictureBox() { Width = 80, Height = 80, Location = new Point(290, 480), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.start_disabled };
            main_screen = new PictureBox() { Width = 400, Height = 300, Location = new Point(10, 40), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.main_screen };           
            whoFirst = new PictureBox() { Width = 70, Height = 70, Location = new Point(210, 230), BackColor = Color.Transparent, SizeMode = PictureBoxSizeMode.StretchImage, Image = Properties.Resources.frame_simple };
            again = new PictureBox() { Width = 80, Height = 80, Location = new Point(410, 400), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.again };

            
            background.Controls.Add(easy);
            background.Controls.Add(normal);
            background.Controls.Add(hard);
            background.Controls.Add(start);
            background.Controls.Add(whoFirst);
            background.Controls.Add(main_screen);
            background.Controls.Add(close); // добавляем крестик для закрытия
            background.Controls.Add(minimize); // добавляем кнопку для сворачивания
            background.Controls.Add(direction); // добавляем стрелки перемещения формы

            AddHandlers();

            //LoadMotionFrames();
        }

        #region Загрузка кадров анимации
        private void LoadMotionFrames()
        {
            ResourceManager imageResources = new ResourceManager(typeof(Properties.Resources));
            for (int i = 0; i < 12; i++)
            {
                imageCross[i] = new Bitmap((Image)imageResources.GetObject(@"frameX" + (i + 1)));
                imageCircle[i] = new Bitmap((Image)imageResources.GetObject(@"frameCircle" + (i + 1)));
            }

            for (int i = 0; i < 16; i++)
            {

                imageField[i] = new Bitmap((Image)imageResources.GetObject(@"fieldFrame" + (i + 1)));
            }

            for (int i = 0; i < 14; i++)
            {
                imageWhoFirst[i] = new Bitmap((Image)imageResources.GetObject(@"frameChoice" + (i + 1)));
            }

            for (int i = 0; i < 26; i++)
            {
                imageCompWin[i] = new Bitmap((Image)imageResources.GetObject(@"compV" + (i + 1)));
            }

            for (int i = 0; i < 24; i++)
            {
                imageManWin[i] = new Bitmap((Image)imageResources.GetObject(@"manV" + (i + 1)));
            }

            for (int i = 0; i < 20; i++)
            {
                imageNobodyWin[i] = new Bitmap((Image)imageResources.GetObject(@"noV" + (i + 1)));
            }
        }
        #endregion

        #region Добавление обработчиков
        private void AddHandlers()
        {
            easy.Click += Easy_Click; //выбор Легкого уровня сложности
            normal.Click += Normal_Click; //выбор Среднего уровня сложности
            hard.Click += Hard_Click; //выбор Тяжелого уровня сложности
            start.Click += Start_Click; //обработчик нажатия на кнопку Пуск
            easy.MouseEnter += Easy_MouseEnter;
            easy.MouseLeave += Easy_MouseLeave;
            normal.MouseEnter += Normal_MouseEnter;
            normal.MouseLeave += Normal_MouseLeave;
            hard.MouseEnter += Hard_MouseEnter;
            hard.MouseLeave += Hard_MouseLeave;
            minimize.Click += Minimize_Click; //обработчик для кнопки сворачивания окна
            close.Click += Close_Click; //обработчик для кнопки закрытия окна
            again.Click += Again_Click;
            minimize.MouseEnter += Minimize_MouseEnter;
            minimize.MouseLeave += Minimize_MouseLeave;
            close.MouseEnter += Close_MouseEnter;
            close.MouseLeave += Close_MouseLeave;
            //обработчики для перемещения окна крестиком
            direction.MouseDown += Direction_MouseDown;
            direction.MouseUp += Direction_MouseUp;
            direction.MouseMove += Direction_MouseMove;
            direction.MouseEnter += Direction_MouseEnter;
            direction.MouseLeave += Direction_MouseLeave;
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.close;
        }

        private void Close_MouseEnter(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.close_hover;
        }

        private void Minimize_MouseLeave(object sender, EventArgs e)
        {
            minimize.Image = Properties.Resources.minimize;
        }

        private void Minimize_MouseEnter(object sender, EventArgs e)
        {
            minimize.Image = Properties.Resources.minimize_hover;
        }

        private void Again_Click(object sender, EventArgs e)
        {
            again.Dispose();
           
            win.Dispose();
            LoadImages();
            isEasy = false;
            isNormal = false;
            isHard = false;
            start.Enabled = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pictureField[i, j].Dispose();
                    pictureField[i, j].Click -= MyPictureBox_Click;
                    pictureField[i, j] = null;
                }
            }
            pictureField = null;
            field.Dispose();
        }
        #endregion

        #region Перемещение формы

        int mouseX;
        int mouseY;
        bool isMouseMoving;

        private void Direction_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseMoving)
            {
                Left = Left + (Cursor.Position.X - mouseX);
                Top = Top + (Cursor.Position.Y - mouseY);

                mouseY = Cursor.Position.Y;
                mouseX = Cursor.Position.X;
            }
        }

        private void Direction_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseMoving = false;
        }

        private void Direction_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
            isMouseMoving = true;
        }
        #endregion

        #region Сворачивание формы
        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Закрытие формы
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Изменение курсора
        private void Direction_MouseLeave(object sender, EventArgs e)
        {
            direction.Image = Properties.Resources.direction1;
            Cursor = Cursors.Default;
        }

        private void Direction_MouseEnter(object sender, EventArgs e)
        {
            direction.Image = Properties.Resources.direction1_hover;
            Cursor = Cursors.Hand;
        }
        #endregion

        #region Взаимодействие с уровнями сложности сложности
        bool isEasy;
        bool isNormal;
        bool isHard;

        private void Hard_MouseLeave(object sender, EventArgs e)
        {
            if (!isHard)
            {
                hard.Image = Properties.Resources.hard;
            }
            else
            {
                hard.Image = Properties.Resources.hard_pressed;
            }
        }

        private void Hard_MouseEnter(object sender, EventArgs e)
        {
            if (!isHard)
            {
                hard.Image = Properties.Resources.hard_hover;
            }
        }

        private void Normal_MouseLeave(object sender, EventArgs e)
        {
            if (!isNormal)
            {
                normal.Image = Properties.Resources.normal;
            }
            else
            {
                normal.Image = Properties.Resources.normal_pressed;
            }
        }

        private void Normal_MouseEnter(object sender, EventArgs e)
        {
            if (!isNormal)
            {
                normal.Image = Properties.Resources.normal_hover;
            }
        }

        private void Easy_MouseLeave(object sender, EventArgs e)
        {
            if (!isEasy)
            {
                easy.Image = Properties.Resources.easy;
            }
            else
            {
                easy.Image = Properties.Resources.easy_pressed;
            }
        }

        private void Easy_MouseEnter(object sender, EventArgs e)
        {
            if (!isEasy)
            {
                easy.Image = Properties.Resources.easy_hover;
            }
        }
        #endregion
    }
}
