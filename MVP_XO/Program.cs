using MVP_XO.Presenter;
using System;
using System.Windows.Forms;

namespace MVP_XO
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();
            new GamePresenter(form);
            Application.Run(form);
        }
    }
}
