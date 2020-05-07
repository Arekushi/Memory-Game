using System;
using System.Drawing;
using System.Drawing.Text;

namespace prjJogoMemoria.MyComponents
{
    public class MyFont
    {
        private static readonly PrivateFontCollection collection = new PrivateFontCollection();

        public static Font Font(String font, float tam)
        {
            collection.AddFontFile(font);
            return new Font(collection.Families[0], tam);
        }
    }
}
