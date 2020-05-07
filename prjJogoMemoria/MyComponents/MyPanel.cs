using System;
using System.Drawing;
using System.Windows.Forms;

namespace prjJogoMemoria.MyComponents
{
    public abstract class MyPanel<T> : Panel where T : MyPanel<T>
    {
        private readonly MyButton[] btnControls = new MyButton[3];
        private static MyPanel<T> instance;

        public static T Instance()
        {
            if (instance == null)
            {
                lock (typeof(T))
                {
                    if (instance == null)
                    {
                        instance = Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
            }

            return (T)instance;
        }

        public static void RemoveInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        public void Controles()
        {
            int x = Width - 50;
            for (int i = 0; i < btnControls.Length; i++)
            {
                btnControls[i] = new MyButton(
                    $"btnControls{i}", MyStrings.controls[i],
                    MyColors.CINZA, Color.GhostWhite,
                    new Point(x, 0), new Size(50, 50),
                    Color.Transparent, 0,
                    5
                );

                x -= 50;
                int r = i;
                btnControls[i].Click += (o, e) =>
                {
                    switch (r)
                    {
                        case 0:
                            Controle.Exit();
                            break;

                        case 1:
                            Controle.DoFullscreen(FormJogoMemoria.Instance());
                            break;

                        case 2:
                            Controle.Minimize(FormJogoMemoria.Instance());
                            break;
                    }
                };

                Controls.Add(btnControls[i]);
            }
        }

    }
}
