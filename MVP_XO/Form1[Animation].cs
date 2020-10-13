using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP_XO
{
    //Анимация
    partial class Form1
    {
        Random rand = new Random();
        private async void PrepareField() 
        {
            await ChangeXPositionAsync();
            await WhoFirstAsync();
            await FieldDrawAsync();
        }
        private async Task ChangeXPositionAsync()
        {
            for (int i = 0; i < 7; i++)
            {
                int x = whoFirst.Location.X;
                int y = whoFirst.Location.Y;
                whoFirst.Location = new Point(x - 8, y - 5);
                whoFirst.Height = whoFirst.Height + 5;
                whoFirst.Width = whoFirst.Width + 5;
                await Task.Delay(50);
            }
        }

        private async Task WhoFirstAsync()
        {
            int count = rand.Next(1, 10);
            InvokeUpPanel();

            int j = 0;

            while (j != count)
            {
                if (j != count)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        whoFirst.Image = imageWhoFirst[i];
                        await Task.Delay(50);
                    }
                    ++j;
                }
                if (j != count)
                {
                    for (int i = 13; i >= 0; i--)
                    {
                        whoFirst.Image = imageWhoFirst[i];
                        await Task.Delay(50);
                    }
                    ++j;
                }
            }

            if (count % 2 == 0)
            {
                ManFirstEvent(this, EventArgs.Empty);
                InvokePanelManFirst();
            }
            else
            {
                CompFirstEvent(this, EventArgs.Empty);
                InvokePanelCompFirst();
            }
        }

        private async Task FieldDrawAsync()
        {
            await Task.Delay(3000);
            whoFirst.Dispose();
            panelup.Dispose();
            paneldown.Dispose();
            CreateField();

            for (int i = 0; i < 16; i++)
            {
                field.Image = imageField[i];
                await Task.Delay(70);
            }

            StartClick(this, EventArgs.Empty);
        }

        private void InvokeUpPanel()
        {
            panelup = new PictureBox() { Width = 300, Height = 80, Location = new Point(50, 30), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.panelup };
            background.Controls.Add(panelup);
        }

        private void InvokePanelManFirst()
        {
            paneldown = new PictureBox() { Width = 300, Height = 300, Location = new Point(50, 300), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.paneldown };
            background.Controls.Add(paneldown);
        }

        private void InvokePanelCompFirst()
        {
            paneldown = new PictureBox() { Width = 300, Height = 300, Location = new Point(50, 300), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent, Image = Properties.Resources.paneldown2 };
            background.Controls.Add(paneldown);
        }

        private void InvokePanelCompWin()
        {
            win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400),BackColor = Color.Transparent};
            background.Controls.Add(win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    win.Image = imageCompWin[i];
                    await Task.Delay(40);
                }
            });
        }

        private void InvokePanelManWin()
        {
            win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400), BackColor = Color.Transparent};
            background.Controls.Add(win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    win.Image = imageManWin[i];
                    await Task.Delay(40);
                }
            });
        }

        private void InvokePanelNobodyWin()
        {
            win = new PictureBox() { Width = 250, Height = 150, Location = new Point(100, 400), BackColor = Color.Transparent };
            background.Controls.Add(win);

            Task.Run(async () =>
            {
                for (int i = 0; i < 26; i++)
                {
                    win.Image = imageNobodyWin[i];
                    await Task.Delay(40);
                }
            });
        }
    }
}
