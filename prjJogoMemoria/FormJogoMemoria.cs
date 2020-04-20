using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjJogoMemoria {
    public partial class FormJogoMemoria : Form {

        private static FormJogoMemoria instance;

        private FormJogoMemoria() {
            InitializeComponent();
        }

        private void FormJogoMemoria_Load(object sender, EventArgs e) {
            InitializeMyPanel();
        }

        /* My methods */
        private void InitializeMyPanel() {
            Controls.Add(StartPanel.Instance());
        }

        /* Singleton */
        public static FormJogoMemoria Instance() {
            if(instance == null) {
                lock (typeof(FormJogoMemoria)) {
                    if (instance == null) {
                        instance = new FormJogoMemoria();
                    }
                }
            }

            return instance;
        }

        public static void RemoveInstance() {
            if (instance != null) {
                instance = null;
            }
        }

    }
}
