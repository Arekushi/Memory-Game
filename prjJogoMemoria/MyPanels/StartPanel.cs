using prjJogoMemoria.MyComponents;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjJogoMemoria
{
    public class StartPanel : MyPanel<StartPanel>
    {
        private readonly Font fntPixel = MyFont.Font(MyStrings.fontPath, 20F);

        private MyPictureBox ptbLoading;
        private CreateAnimation myAnime;

        private StartPanel()
        {
            Size = new Size(FormJogoMemoria.Instance().Width, FormJogoMemoria.Instance().Height);
            Anchor = AnchorStyles.None;
            Name = "startPanel";
            Location = new Point(0, 0);
            BackColor = MyColors.CINZA;
            Font = fntPixel;
            TabIndex = 0;

            Animaton();
            _ = TrocarTela();
        }

        private void Animaton()
        {
            ptbLoading = new MyPictureBox(
                "ptbLoading", new Point((Width - 100) / 2, (Height - 74) / 2),
                new Size(100, 74), PictureBoxSizeMode.Zoom
            );

            myAnime = new CreateAnimation(ptbLoading, 20, MyStrings.adventureLoading, 30);
            myAnime.StartAnimationLoop();

            Controls.Add(ptbLoading);
        }

        private async Task TrocarTela()
        {
            await Task.Delay(3000);
            FormJogoMemoria.Instance().Controls.Add(MainPanel.Instance());
            FormJogoMemoria.Instance().Controls.Remove(this);
            RemoveInstance();
        }

    }
}
