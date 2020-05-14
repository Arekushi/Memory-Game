using prjJogoMemoria.Database;
using prjJogoMemoria.Model;
using prjJogoMemoria.MyComponents;
using prjJogoMemoria.MyPanels;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace prjJogoMemoria
{
    public class Game
    {
        #region Variables

        public string NamePlayer { get; set; }

        public int Difficulties { get; set; }

        public int Score { get; set; }

        public string[] ListCards { get; set; }

        public int Hits { get; set; }

        public int Attacks { get; set; }

        public Click[] Click { get; set; } = new Click[2];

        private static Game instance;
        private Card[,] cards;

        #endregion

        #region Methods

        private Game()
        {
            Hits = 0;
            Attacks = 0;
            Score = 0;

            ResetClick();
        }

        public Card[,] ColumnSize()
        {
            switch (Difficulties)
            {
                case 0:
                    cards = new Card[3, 4];
                    break;

                case 1:
                    cards = new Card[3, 6];
                    break;

                case 2:
                    cards = new Card[4, 5];
                    break;
            }

            return cards;
        }

        public int ReturnScore()
        {
            switch (Difficulties)
            {
                case 0:
                    Score = 3000;
                    break;

                case 1:
                    Score = 5000;
                    break;

                case 2:
                    Score = 5000;
                    break;
            }

            return Score;
        }

        private string[] GetImages()
        {
            string[] images = new string[22];

            for (int i = 0; i < 22; i++)
            {
                images[i] = $"{MyStrings.weaponsPath}{i + 1}.png";
            }

            return images;
        }

        private string[] Duplicate(string[] oldArrayCards)
        {
            string[] newArrayCards = new string[oldArrayCards.Length * 2];
            int index = 0;

            for (int i = 0; i < oldArrayCards.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    newArrayCards[index] = oldArrayCards[i];
                    index++;
                }
            }

            return newArrayCards;
        }

        public string[] Shuffle()
        {
            Random random = new Random();
            string[] allCards = GetImages().OrderBy(x => random.Next()).ToArray();
            string[] halfCards = new string[GetHalfCards()];
            Array.Copy(allCards, halfCards, halfCards.Length);

            return Duplicate(halfCards).OrderBy(x => random.Next()).ToArray();
        }

        public void ResetClick()
        {
            for (int i = 0; i < Click.Length; i++)
            {
                Click[i] = new Click();
            }
        }

        public void Missed(MyLabel lblScore, MyLabel lblHits, MyPictureBox ptbCharacter)
        {
            Score -= 300;
            Hits++;

            lblScore.Text = $"Score: {Score}";
            lblHits.Text = $"Hits: {Hits}";
            ptbCharacter.Size = new Size(125, 92);

            if (Score > 0)
            {
                GamePanel.Instance().idleAnime.StopAnimation();
                GamePanel.Instance().myAnime = new CreateAnimation(ptbCharacter, 3, MyStrings.adventureHit, 5);
                GamePanel.Instance().myAnime.StartAnimation();
            }
            else
            {
                GamePanel.Instance().idleAnime.StopAnimation();
                GamePanel.Instance().myAnime = new CreateAnimation(ptbCharacter, 7, MyStrings.adventureDie, 5);
                GamePanel.Instance().myAnime.StartAnimation();

                Popup("Você morreu... Deseja continuar?", "Game Over");
            }

        }

        public void Success(MyLabel lblAttacks, MyPictureBox ptbCharacter)
        {
            Attacks++;
            lblAttacks.Text = $"Attacks: {Attacks}";
            ptbCharacter.Size = new Size(125, 92);

            GamePanel.Instance().idleAnime.StopAnimation();
            GamePanel.Instance().myAnime = new CreateAnimation(ptbCharacter, 13, MyStrings.adventureAttack, 15);
            GamePanel.Instance().myAnime.StartAnimation();

            if (Attacks == GetHalfCards())
            {
                SetHighScore();
                Popup("Você completou, deseja reiniciar?", "Parabéns");
            }
        }

        private void SetHighScore()
        {
            Player player = new Player() {
                NamePlayer = NamePlayer,
                HighScore = Score
            };

            SQLiteDataAccess.SetHighScore(player);
            MessageBox.Show("Novo recorde registrado");
        }

        private void Popup(string msg, string title)
        {
            var result = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel);

            switch (result)
            {
                case DialogResult.Yes:
                    FormJogoMemoria.Instance().Controls.Remove(GamePanel.Instance());
                    GamePanel.RemoveInstance();

                    int d = Difficulties;
                    string n = NamePlayer;

                    RemoveInstance();
                    Instance().Difficulties = d;
                    Instance().NamePlayer = n;
                    FormJogoMemoria.Instance().Controls.Add(GamePanel.Instance());
                    break;

                case DialogResult.No:
                    FormJogoMemoria.Instance().Controls.Remove(GamePanel.Instance());
                    GamePanel.RemoveInstance();
                    RemoveInstance();
                    FormJogoMemoria.Instance().Controls.Add(MainPanel.Instance());
                    break;

                case DialogResult.Cancel:
                    Application.Exit();
                    break;
            }
        }

        private int GetHalfCards()
        {
            return ColumnSize().GetLength(0) * ColumnSize().GetLength(1) / 2;
        }

        #endregion

        #region Singleton

        public static Game Instance()
        {
            if (instance == null)
            {
                lock (typeof(Game))
                {
                    if (instance == null)
                    {
                        instance = new Game();
                    }
                }
            }

            return instance;
        }

        public static void RemoveInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        #endregion
    }
}
