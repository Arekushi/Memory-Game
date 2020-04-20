using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace prjJogoMemoria {
    public static class Controle {

        private static bool isFull = false;

        public static void DoFullscreen(Form form) {
            if (isFull == false) {
                Fullscreen(form);
                isFull = true;
            } else {
                Normal(form);
                isFull = false;
            }
        }

        private static void Fullscreen(Form form) {
            if (form.WindowState == FormWindowState.Normal) {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        private static void Normal(Form form) {
            form.WindowState = FormWindowState.Normal;
        }

        public static void Minimize(Form form) {
            if (form.WindowState == FormWindowState.Normal || form.WindowState == FormWindowState.Maximized) {
                form.WindowState = FormWindowState.Minimized;
            } else if (form.WindowState == FormWindowState.Minimized) {
                form.WindowState = FormWindowState.Normal;
            }
        }

        public static void Exit() {
            Application.Exit();
        }
    }
}
