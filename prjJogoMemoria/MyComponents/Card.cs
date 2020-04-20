using prjJogoMemoria.MyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using prjJogoMemoria.MyPanels;
using System.Threading;

namespace prjJogoMemoria {
    public class Card : Button {

		private readonly int conerSize = 10;
		private readonly Color conerColor = Color.Transparent;

		public Bitmap ImagemFrente { get; set; }
        public Bitmap ImagemCostas { get; set; }
        public string IdCarta { get; set; }
        public bool Virado { get; set; }
        public int Indice { get; set; }

        public Card(string name, Point point, Size size, Bitmap frente, string idCarta, int indice) {
            Name = name;
            Location = point;
            Size = size;
			TabIndex = 0;
			TabStop = false;
			BackColor = MyColors.LARANJA;
			Image = frente;

			ImagemFrente = frente;
			IdCarta = idCarta;
			Indice = indice;
		}

		public void VirarCarta() {
			Image = ImagemFrente;
		}

		public async Task DesvirarCarta() {
			await Task.Delay(500);
			Image = null;
		}

		protected override void OnPaint(PaintEventArgs pevent) {
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
