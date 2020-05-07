using System;
using System.Windows.Forms;

namespace prjJogoMemoria
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FormJogoMemoria.Instance());
        }
    }
}
