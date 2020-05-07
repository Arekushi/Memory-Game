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
        public int Dificuldade { get; set; }

        public int Score { get; set; }

        public string[] ListCards { get; set; }

        public int Hits { get; set; }

        public int Attacks { get; set; }

        public Click[] Click { get; set; } = new Click[2];

        private static Game instance;
        private Card[,] cartas;

        private Game()
        {
            Hits = 0;
            Attacks = 0;
            Score = 0;

            ResetClick();
        }

        public Card[,] TamanhoColunaCards()
        {
            switch (Dificuldade)
            {
                case 0:
                    cartas = new Card[3, 4];
                    break;

                case 1:
                    cartas = new Card[3, 6];
                    break;

                case 2:
                    cartas = new Card[4, 5];
                    break;
            }

            return cartas;
        }

        public int ReturnScore()
        {
            switch (Dificuldade)
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

        private string[] PegarImagens()
        {
            string[] imagens = new string[22];

            for (int i = 0; i < 22; i++)
            {
                imagens[i] = $"{MyStrings.weaponsPath}{i + 1}.png";
            }

            return imagens;
        }

        private string[] Duplicar(string[] array1)
        {
            string[] array2 = new string[array1.Length * 2];
            int index = 0;

            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    array2[index] = array1[i];
                    index++;
                }
            }

            return array2;
        }

        public string[] Embaralha()
        {
            int qtdCartas = TamanhoColunaCards().GetLength(0) * TamanhoColunaCards().GetLength(1);
            int metadeQtdCartas = qtdCartas / 2;
            Random random = new Random();

            string[] todasCartas = PegarImagens().OrderBy(x => random.Next()).ToArray();

            string[] metadeCartas = new string[metadeQtdCartas];
            Array.Copy(todasCartas, metadeCartas, metadeQtdCartas);

            string[] cartasDuplicadas = Duplicar(metadeCartas).OrderBy(x => random.Next()).ToArray();

            return cartasDuplicadas;
        }

        public void ResetClick()
        {
            for (int i = 0; i < Click.Length; i++)
            {
                Click[i] = new Click();
            }
        }

        public void Errou(MyLabel lblScore, MyLabel lblHits, MyPictureBox ptbPersonagem)
        {
            Score -= 300;
            Hits++;

            lblScore.Text = $"Score: {Score}";
            lblHits.Text = $"Hits: {Hits}";
            ptbPersonagem.Size = new Size(125, 92);

            if (Score > 0)
            {
                GamePanel.Instance().idleAnime.StopAnimation();
                GamePanel.Instance().myAnime = new CreateAnimation(ptbPersonagem, 3, MyStrings.adventureHit, 5);
                GamePanel.Instance().myAnime.StartAnimation();
            }
            else
            {
                GamePanel.Instance().idleAnime.StopAnimation();
                GamePanel.Instance().myAnime = new CreateAnimation(ptbPersonagem, 7, MyStrings.adventureDie, 5);
                GamePanel.Instance().myAnime.StartAnimation();

                Popup("Você morreu... Deseja continuar?", "Game Over");
            }

        }

        public void Acertou(MyLabel lblAttacks, MyPictureBox ptbPersonagem)
        {
            Attacks++;
            lblAttacks.Text = $"Attacks: {Attacks}";
            ptbPersonagem.Size = new Size(125, 92);

            GamePanel.Instance().idleAnime.StopAnimation();
            GamePanel.Instance().myAnime = new CreateAnimation(ptbPersonagem, 13, MyStrings.adventureAttack, 15);
            GamePanel.Instance().myAnime.StartAnimation();

            if (Attacks == (TamanhoColunaCards().GetLength(0) * TamanhoColunaCards().GetLength(1)) / 2)
            {
                AtualizarHighScore();
                Popup("Você completou, deseja reiniciar?", "Parabéns");
            }
        }

        private void AtualizarHighScore()
        {
            foreach (var item in SQLiteDataAccess.SelectHighScore())
            {
                if (item.HighScore < Score)
                {
                    Player player = new Player
                    {
                        HighScore = Score
                    };

                    SQLiteDataAccess.UpdateHighScore(player);
                    MessageBox.Show("Novo recorde registrado");
                }
            }
        }

        private void Popup(string msg, string title)
        {
            var result = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel);

            switch (result)
            {
                case DialogResult.Yes:
                    FormJogoMemoria.Instance().Controls.Remove(GamePanel.Instance());
                    GamePanel.RemoveInstance();
                    int d = Dificuldade;
                    RemoveInstance();
                    Instance().Dificuldade = d;
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
    }
}
