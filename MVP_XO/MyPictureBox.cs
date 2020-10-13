using System.Windows.Forms;

namespace MVP_XO
{
    class MyPictureBox : PictureBox
    {
        public int X { get; }
        public int Y { get; }

        public MyPictureBox(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
