using prjJogoMemoria.Database;
using prjJogoMemoria.Model;
using prjJogoMemoria.MyComponents;
using prjJogoMemoria.MyPanels;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;

namespace prjJogoMemoria
{
    public class MainPanel : MyPanel<MainPanel>
    {
        #region Variables

        private readonly MyButton[] btnDifficulties = new MyButton[3];
        private readonly Font fntPixel = MyFont.Font(MyStrings.fontPath, 20F);

        private MyButton btnPlay;
        private MyLabel lblPixelMemory;
        private MyLabel lblHighScore;
        private MyPictureBox ptbLogo;
        private CreateAnimation myAnime;
        private SoundPlayer musicBackground;

        #endregion

        #region Methods

        private MainPanel()
        {
            Size = new Size(FormJogoMemoria.Instance().Width, FormJogoMemoria.Instance().Height);
            Anchor = AnchorStyles.None;
            Name = "mainPanel";
            Location = new Point(0, 0);
            BackColor = MyColors.GRAY;
            Font = fntPixel;
            TabIndex = 0;

            Song();
            Controllers();
            PixelMemory();
            Weapons();
            Difficulties();
            Play();
            HighScore();
        }

        private void Song()
        {
            musicBackground = new SoundPlayer(MyStrings.openingSongPath);
            musicBackground.PlayLooping();
        }

        private void PixelMemory()
        {
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

        private void Weapons()
        {
            ptbLogo = new MyPictureBox(
                "ptbLogo", new Point((Width - 80) / 2, ((Height - 80) / 2) - 50),
                new Size(80, 80), MyColors.ORANGE,
                PictureBoxSizeMode.Zoom, 10,
                Color.Transparent
            );

            myAnime = new CreateAnimation(ptbLogo, 22, MyStrings.weaponsPath, 1);
            myAnime.StartAnimationLoop();
            Controls.Add(ptbLogo);
        }

        private void Play()
        {
            btnPlay = new MyButton(
                "btnPlay", "Play",
                MyColors.ORANGE, Color.GhostWhite,
                new Point((Width - 100) / 2, ((Height - 50) / 2) + 100), new Size(100, 50),
                MyColors.ORANGE, 2,
                10
            );

            bool click = true;
            btnPlay.Click += (o, e) =>
            {
                if (click)
                {
                    VisibilityDifficulties(click);
                    click = false;
                }
                else
                {
                    VisibilityDifficulties(click);
                    click = true;
                }
            };

            Controls.Add(btnPlay);
        }

        private void HighScore()
        {
            foreach (Player player in SQLiteDataAccess.SelectHighScore())
            {
                lblHighScore = new MyLabel(
                    "lblHighScore", $"High Score: {player.HighScore} - {player.NamePlayer}",
                    Color.Transparent, Color.GhostWhite,
                    new Point(0, 0),
                    MyFont.Font(MyStrings.fontPath, 30F)
                );

                lblHighScore.Location = new Point(0, Height - (lblHighScore.Height * 2));
            }

            Controls.Add(lblHighScore);
        }

        private void Difficulties()
        {
            int X = ((Width - 150) / 3) - 25;

            for (int i = 0; i < btnDifficulties.Length; i++)
            {
                btnDifficulties[i] = new MyButton(
                    $"btnDifficulties{i}", MyStrings.Difficulties[i],
                    MyColors.ORANGE, Color.GhostWhite,
                    new Point(X, ((Height - 50) / 2) + 175), new Size(100, 50),
                    MyColors.ORANGE, 2, 10
                )
                {
                    Visible = false
                };

                int r = i;
                btnDifficulties[i].Click += (o, e) =>
                {
                    string userAnswer = Interaction.InputBox("Escreva um nome", "");

                    if(userAnswer != string.Empty)
                    {
                        Game.Instance().NamePlayer = userAnswer;
                        Game.Instance().Difficulties = r;
                        FormJogoMemoria.Instance().Controls.Add(GamePanel.Instance());
                        FormJogoMemoria.Instance().Controls.Remove(this);
                        RemoveInstance();
                    }

                };

                X += 125;
                Controls.Add(btnDifficulties[i]);
            }
        }

        private void VisibilityDifficulties(bool click)
        {
            for (int i = 0; i < btnDifficulties.Length; i++)
            {
                btnDifficulties[i].Visible = click;
            }
        }

        #endregion
    }
}
