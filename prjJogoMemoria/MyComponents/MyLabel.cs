using System.Drawing;
using System.Windows.Forms;

namespace prjJogoMemoria.MyComponents
{
    public class MyLabel : Label
    {
        public MyLabel(string name, string text, Color bgColor, Color frColor, Point point)
        {
            Name = name;
            Text = text;
            BackColor = bgColor;
            ForeColor = frColor;
            Location = point;
            TabIndex = 0;
            AutoSize = true;
            TabStop = false;
            TextAlign = ContentAlignment.MiddleCenter;
        }

        public MyLabel(string name, string text, Color bgColor, Color frColor, Point point, Font font)
        {
            Name = name;
            Text = text;
            BackColor = bgColor;
            ForeColor = frColor;
            Location = point;
            TabIndex = 0;
            AutoSize = true;
            TabStop = false;
            Font = font;
            TextAlign = ContentAlignment.MiddleCenter;
        }

    }
}
