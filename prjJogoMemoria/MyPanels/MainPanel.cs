using prjJogoMemoria.MyComponents;
using prjJogoMemoria.MyPanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using prjJogoMemoria.Database;
using prjJogoMemoria.Model;
using System.Media;

namespace prjJogoMemoria {
    public class MainPanel : MyPanel<MainPanel> {

        private readonly MyButton[] btnDificuldades = new MyButton[3];
        private readonly Font fntPixel = MyFont.Font(MyStrings.fontPath, 20F);

        private MyButton btnPlay;
        private MyLabel lblPixelMemory;
        private MyLabel lblHighScore;
        private MyPictureBox ptbLogo;
        private CreateAnimation myAnime;
        private SoundPlayer musicaFundo;

        private MainPanel() {
            Size = new Size(FormJogoMemoria.Instance().Width, FormJogoMemoria.Instance().Height);
            Anchor = AnchorStyles.None;
            Name = "mainPanel";
            Location = new Point(0, 0);
            BackColor = MyColors.CINZA;
            Font = fntPixel;
            TabIndex = 0;

            Song();
            Controles();
            PixelMemory();
            Weapons();
            Dificuldades();
            Play();
            HighScore();
        }

        private void Song() {
            musicaFundo = new SoundPlayer(MyStrings.openingSongPath);
            musicaFundo.PlayLooping();
        }

        private void PixelMemory() {
            lblPixelMemory = new MyLabel(
                "lblPixelMemory", "Pixel Memory",
                Color.Transparent, Color.GhostWhite,
                new Point(0, 0),
                MyFont.Font(MyStrings.fontPath, 35F)
            );

            lblPixelMemory.Location = new Point((Width / 2) - 125,
                ((Height - lblPixelMemory.Height) / 2) - 150);

            Controls.Add(lblPixelMemory);
        }

        private void Weapons() {
            ptbLogo = new MyPictureBox(
                "ptbLogo", new Point((Width - 80) / 2, ((Height - 80) / 2) - 50),
                new Size(80, 80), MyColors.LARANJA,
                PictureBoxSizeMode.Zoom, 10,
                Color.Transparent
            );

            myAnime = new CreateAnimation(ptbLogo, 22, MyStrings.weaponsPath, 1);
            myAnime.StartAnimationLoop();

            Controls.Add(ptbLogo);
        }

        private void Play() {
            btnPlay = new MyButton(
                "btnPlay", "Play",
                MyColors.LARANJA, Color.GhostWhite,
                new Point((Width - 100) / 2, ((Height - 50) / 2) + 100), new Size(100, 50),
                MyColors.LARANJA, 2,
                10
            );

            bool click = true;
            btnPlay.Click += (o, e) => {
                if (click) {
                    VisibilidadeDificuldades(click);
                    click = false;
                } else {
                    VisibilidadeDificuldades(click);
                    click = true;
                }
            };

            Controls.Add(btnPlay);
        }

        private void HighScore() {
            foreach(Player play in SQLiteDataAccess.SelectHighScore()) {
                lblHighScore = new MyLabel(
                    "lblHighScore", $"High Score: {play.HighScore.ToString()}",
                    Color.Transparent, Color.GhostWhite,
                    new Point(0, 0),
                    MyFont.Font(MyStrings.fontPath, 30F)
                );

                lblHighScore.Location = new Point(0, Height - (lblHighScore.Height * 2));
            }

            Controls.Add(lblHighScore);
        }

        private void Dificuldades() {
            int X = ((Width - 150) / 3) - 25;

            for (int i = 0; i < btnDificuldades.Length; i++) {
                btnDificuldades[i] = new MyButton(
                    $"btnDificuldades{i}", MyStrings.dificuldades[i],
                    MyColors.LARANJA, Color.GhostWhite,
                    new Point(X, ((Height - 50) / 2) + 175), new Size(100, 50),
                    MyColors.LARANJA, 2,
                    10
                ) {
                    Visible = false
                };

                int r = i;
                btnDificuldades[i].Click += (o, e) => {
                    Game.Instance().Dificuldade = r;
                    FormJogoMemoria.Instance().Controls.Add(GamePanel.Instance());
                    FormJogoMemoria.Instance().Controls.Remove(this);
                    RemoveInstance();
                };

                X += 125;
                Controls.Add(btnDificuldades[i]);
            }
        }

        /* Método Dificuldades */
        private void VisibilidadeDificuldades(bool click) {
            for (int i = 0; i < btnDificuldades.Length; i++) {
                btnDificuldades[i].Visible = click;
            }
        }

    }
}
