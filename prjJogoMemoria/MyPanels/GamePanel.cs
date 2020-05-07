using prjJogoMemoria.MyComponents;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjJogoMemoria.MyPanels
{
    public class GamePanel : MyPanel<GamePanel>
    {

        private static Game game;
        private readonly Font fntPixel = MyFont.Font(MyStrings.fontPath, 20F);
        private readonly Card[,] cartas = Game.Instance().TamanhoColunaCards();

        private SoundPlayer musicaFundo;
        private MyPictureBox ptbPersonagem;
        private MyLabel lblScore;
        private MyLabel lblHits;
        private MyLabel lblAttacks;

        public CreateAnimation idleAnime;
        public CreateAnimation myAnime;

        private GamePanel()
        {
            Size = new Size(FormJogoMemoria.Instance().Width, FormJogoMemoria.Instance().Height);
            Anchor = AnchorStyles.None;
            Name = "gamePanel";
            Location = new Point(0, 0);
            BackColor = MyColors.CINZA;
            Font = fntPixel;
            TabIndex = 0;
            game = Game.Instance();

            Song();
            Controles();
            _ = CardsAsync();
            Personagem();
            Score();
            Attacks();
            Hits();
        }

        private void Song()
        {
            musicaFundo = new SoundPlayer(MyStrings.battleSongPath);
            musicaFundo.PlayLooping();
        }

        private async Task CardsAsync()
        {
            game.ListCards = game.Embaralha();
            int index = 0;
            int y = 70;

            for (int i = 0; i < cartas.GetLength(0); i++)
            {
                int x = (Width - (cartas.GetLength(1) * 80)) / 2;

                for (int j = 0; j < cartas.GetLength(1); j++)
                {
                    cartas[i, j] = new Card(
                        $"carta{index}", new Point(x, y),
                        new Size(80, 80), new Bitmap(game.ListCards[index]),
                        game.ListCards[index], index
                    );

                    index++;
                    x += 80;

                    cartas[i, j].Click += (o, e) => ClickCarta(o);

                    Controls.Add(cartas[i, j]);
                }
                y += 80;
            }

            await DesvirarTodasCartas(index);
        }

        private void Personagem()
        {
            ptbPersonagem = new MyPictureBox(
                "ptbPersonagem", new Point((Width - 54) / 2, Height - 100),
                new Size(67, 75), PictureBoxSizeMode.Zoom
            );

            idleAnime = new CreateAnimation(ptbPersonagem, 4, MyStrings.adventureIdle, 10);
            idleAnime.StartAnimationLoop();

            Controls.Add(ptbPersonagem);
        }

        private void Score()
        {
            lblScore = new MyLabel(
                "lblHighScore", $"Score: {game.ReturnScore()}",
                Color.Transparent, Color.GhostWhite,
                new Point(0, 0),
                MyFont.Font(MyStrings.fontPath, 30F)
            );

            lblScore.Location = new Point(0, Height - (lblScore.Height * 2));


            Controls.Add(lblScore);
        }

        private void Hits()
        {
            lblHits = new MyLabel(
                "lblHighScore", $"Hits: {game.Hits}",
                Color.Transparent, Color.GhostWhite,
                new Point(0, 0),
                MyFont.Font(MyStrings.fontPath, 30F)
            );

            lblHits.Location =
                new Point((Width - 130), Height - (lblHits.Height * 2) - lblAttacks.Height);


            Controls.Add(lblHits);
        }

        private void Attacks()
        {
            lblAttacks = new MyLabel(
                "lblHighScore", $"Attacks: {game.Attacks}",
                Color.Transparent, Color.GhostWhite,
                new Point(0, 0),
                MyFont.Font(MyStrings.fontPath, 30F)
            );

            lblAttacks.Location =
                new Point((Width - 185), Height - (lblAttacks.Height * 2));

            Controls.Add(lblAttacks);
        }

        /* Métodos Carta */
        public void ClickCarta(object o)
        {
            var carta = (Card)o;

            /* Verifica se já não houve o primeiro click */
            /* Verifica se o segundo click não é da mesma carta do primeiro */
            /* Verifica se as duas são iguais */

            if (game.Click[0].IdClick != null)
            {
                if (game.Click[0].IndiceClick != carta.Indice)
                {
                    game.Click[1].IdClick = carta.IdCarta;
                    game.Click[1].IndiceClick = carta.Indice;
                    VirarCarta(1);

                    if (game.Click[0].IdClick == game.Click[1].IdClick)
                    {
                        DesativarCartas();
                        game.Acertou(lblAttacks, ptbPersonagem);
                        game.ResetClick();

                    }
                    else
                    {
                        DesvirarCartas();
                        game.Errou(lblScore, lblHits, ptbPersonagem);
                        game.ResetClick();
                    }
                }

            }
            else
            {
                game.Click[0].IdClick = carta.IdCarta;
                game.Click[0].IndiceClick = carta.Indice;
                VirarCarta(0);
            }
        }

        private void VirarCarta(int index)
        {
            cartas
            .OfType<Card>()
            .ToList()
            .FirstOrDefault(c => c.Indice == game.Click[index].IndiceClick)
            .VirarCarta();
        }

        private void DesvirarCartas()
        {
            for (int p = 0; p < 2; p++)
            {
                _ = cartas
                .OfType<Card>()
                .ToList()
                .FirstOrDefault(m => m.Indice == game.Click[p].IndiceClick)
                .DesvirarCarta();
            }
        }

        private async Task DesvirarTodasCartas(int qtdCartas)
        {
            await Task.Delay(100);

            for (int i = 0; i < qtdCartas; i++)
            {
                _ = cartas
                .OfType<Card>()
                .ToList()
                .First(m => m.Indice == i)
                .DesvirarCarta();
            }

        }

        private void DesativarCartas()
        {
            for (int k = 0; k < 2; k++)
            {
                cartas
                .OfType<Card>()
                .ToList()
                .FirstOrDefault(c => c.Indice == game.Click[k].IndiceClick)
                .Enabled = false;
            }
        }

    }
}
