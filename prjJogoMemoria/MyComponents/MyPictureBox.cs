using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace prjJogoMemoria.MyComponents
{
    public class MyPictureBox : PictureBox
    {
        private readonly int conerSize = 1;
        private readonly Color conerColor = Color.Transparent;

        public MyPictureBox(string name, Point point, Size size)
        {
            Name = name;
            Location = point;
            Size = size;
            TabIndex = 0;
            TabStop = false;
            SizeMode = PictureBoxSizeMode.Zoom;
        }

        public MyPictureBox(string name, Point point, Size size, PictureBoxSizeMode mode)
        {
            Name = name;
            Location = point;
            Size = size;
            TabIndex = 0;
            TabStop = false;
            SizeMode = mode;
        }

        public MyPictureBox(string name, Point point, Size size, Color bgColor)
        {
            Name = name;
            Location = point;
            Size = size;
            TabIndex = 0;
            TabStop = false;
            SizeMode = PictureBoxSizeMode.Zoom;
            BackColor = bgColor;
        }

        public MyPictureBox(string name, Point point, Size size, Color bgColor, PictureBoxSizeMode mode)
        {
            Name = name;
            Location = point;
            Size = size;
            TabIndex = 0;
            TabStop = false;
            SizeMode = mode;
            BackColor = bgColor;
        }

        public MyPictureBox(string name, Point point, Size size, Color bgColor,
                            PictureBoxSizeMode mode, int conerSize, Color conerColor)
        {
            Name = name;
            Location = point;
            Size = size;
            TabIndex = 0;
            TabStop = false;
            SizeMode = mode;
            BackColor = bgColor;
            this.conerSize = conerSize;
            this.conerColor = conerColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // to draw the control using base OnPaint
            base.OnPaint(pevent);
            //to modify the corner radius
            int CornerRadius = conerSize;

            Pen DrawPen = new Pen(conerColor);
            GraphicsPath gfxPath_mod = new GraphicsPath();

            int top = 0;
            int left = 0;
            int right = Width;
            int bottom = Height;

            gfxPath_mod.AddArc(left, top, CornerRadius, CornerRadius, 180, 90);
            gfxPath_mod.AddArc(right - CornerRadius, top, CornerRadius, CornerRadius, 270, 90);
            gfxPath_mod.AddArc(right - CornerRadius, bottom - CornerRadius,
                CornerRadius, CornerRadius, 0, 90);
            gfxPath_mod.AddArc(left, bottom - CornerRadius, CornerRadius, CornerRadius, 90, 90);

            gfxPath_mod.CloseAllFigures();

            pevent.Graphics.DrawPath(DrawPen, gfxPath_mod);

            int inside = 1;

            Pen newPen = new Pen(conerColor, conerSize);
            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(left + inside + 1, top + inside, CornerRadius, CornerRadius, 180, 100);

            gfxPath.AddArc(right - CornerRadius - inside - 2,
                top + inside, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(right - CornerRadius - inside - 2,
                bottom - CornerRadius - inside - 1, CornerRadius, CornerRadius, 0, 90);

            gfxPath.AddArc(left + inside + 1,
            bottom - CornerRadius - inside, CornerRadius, CornerRadius, 95, 95);
            pevent.Graphics.DrawPath(newPen, gfxPath);

            Region = new Region(gfxPath_mod);
        }

    }
}
