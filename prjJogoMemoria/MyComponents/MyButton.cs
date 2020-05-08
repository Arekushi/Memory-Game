using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace prjJogoMemoria.MyComponents
{
	public class MyButton : Button
	{
		private readonly int conerSize;
		private readonly Color conerColor;

		public MyButton(string name, string text, Color bgColor, Color frColor, 
			Point point, Size size, Color border, int bSize, int conerSize)
		{
			Name = name;
			Text = text;
			BackColor = bgColor;
			ForeColor = frColor;
			Location = point;
			Size = size;
			TabIndex = 0;
			TabStop = false;
			Visible = true;
			FlatStyle = FlatStyle.Flat;

			if (border == Color.Transparent)
			{
				conerColor = Color.Transparent;
				FlatAppearance.BorderColor = bgColor;
			}
			else
			{
				conerColor = border;
				FlatAppearance.BorderColor = border;
			}

			FlatAppearance.BorderSize = bSize;
			this.conerSize = conerSize;
		}

		public MyButton(string name, string text, Color bgColor, Color frColor, Point point, Size size, Font font)
		{
			Name = name;
			Text = text;
			BackColor = bgColor;
			ForeColor = frColor;
			Location = point;
			Size = size;
			TabIndex = 0;
			TabStop = false;
			Font = font;
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
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

			Pen newPen = new Pen(conerColor, FlatAppearance.BorderSize);
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
