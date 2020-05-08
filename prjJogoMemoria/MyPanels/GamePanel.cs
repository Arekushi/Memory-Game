using prjJogoMemoria.MyComponents;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjJogoMemoria.MyPanels
{
    public class GamePanel : MyPanel<GamePanel>
    {
        #region Variables

        private static Game game;
        private readonly Font fntPixel = MyFont.Font(MyStrings.fontPath, 20F);
        private readonly Card[,] cards = Game.Instance().ColumnSize();

        private SoundPlayer musicBackground;
        private MyPictureBox ptbCharacter;
        private MyLabel lblScore;
        private MyLabel lblHits;
        private MyLabel lblAttacks;

        public CreateAnimation idleAnime;
        public CreateAnimation myAnime;

        #endregion

        #region Methods

        private GamePanel()
        {
            Size = new Size(FormJogoMemoria.Instance().Width, FormJogoMemoria.Instance().Height);
            Anchor = AnchorStyles.None;
            Name = "gamePanel";
            Location = new Point(0, 0);
            BackColor = MyColors.GRAY;
            Font = fntPixel;
            TabIndex = 0;
            game = Game.Instance();

            Song();
            Controllers();
            _ = CardsAsync();
            Character();
            Score();
            Attacks();
            Hits();
        }

        private void Song()
        {
            musicBackground = new SoundPlayer(MyStrings.battleSongPath);
            musicBackground.PlayLooping();
        }

        private async Task CardsAsync()
        {
            game.ListCards = game.Shuffle();
            int index = 0;
            int y = 70;

            for (int i = 0; i < cards.GetLength(0); i++)
            {
                int x = (Width - (cards.GetLength(1) * 80)) / 2;
                for (int j = 0; j < cards.GetLength(1); j++)
                {
                    cards[i, j] = new Card(
                        $"carta{index}", new Point(x, y),
                        new Size(80, 80), new Bitmap(game.ListCards[index]),
                        game.ListCards[index], index
                    );

                    index++;
                    x += 80;

                    cards[i, j].Click += (o, e) => CardClick(o);
                    Controls.Add(cards[i, j]);
                }
                y += 80;
            }

            await UntapAllCards(index);
        }

        private void Character()
        {
            ptbCharacter = new MyPictureBox(
                "ptbCharacter", new Point((Width - 54) / 2, Height - 100),
                new Size(67, 75), PictureBoxSizeMode.Zoom
            );

            idleAnime = new CreateAnimation(ptbCharacter, 4, MyStrings.adventureIdle, 10);
            idleAnime.StartAnimationLoop();
            Controls.Add(ptbCharacter);
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

        #endregion

        #region Cards

        public void CardClick(object @object)
        {
            var card = (Card) @object;

            /* Verifica se já não houve o primeiro click */
            /* Verifica se o segundo click não é da mesma card do primeiro */
            /* Verifica se as duas são iguais */

            if (game.Click[0].IdClick != null)
            {
                if (game.Click[0].IndiceClick != card.Index)
                {
                    game.Click[1].IdClick = card.IdCarta;
                    game.Click[1].IndiceClick = card.Index;
                    TapCard(1);

                    if (game.Click[0].IdClick == game.Click[1].IdClick)
                    {
                        DisableCards();
                        game.Success(lblAttacks, ptbCharacter);
                        game.ResetClick();

                    }
                    else
                    {
                        UntapCards();
                        game.Missed(lblScore, lblHits, ptbCharacter);
                        game.ResetClick();
                    }
                }

            }
            else
            {
                game.Click[0].IdClick = card.IdCarta;
                game.Click[0].IndiceClick = card.Index;
                TapCard(0);
            }
        }

        private void TapCard(int index)
        {
            cards
            .OfType<Card>()
            .ToList()
            .FirstOrDefault(c => c.Index == game.Click[index].IndiceClick)
            .TapCard();
        }

        private void UntapCards()
        {
            for (int i = 0; i < 2; i++)
            {
                _ = cards
                .OfType<Card>()
                .ToList()
                .FirstOrDefault(c => c.Index == game.Click[i].IndiceClick)
                .UntapCard();
            }
        }

        private async Task UntapAllCards(int qtdCartas)
        {
            await Task.Delay(100);

            for (int i = 0; i < qtdCartas; i++)
            {
                _ = cards
                .OfType<Card>()
                .ToList()
                .First(c => c.Index == i)
                .UntapCard();
            }

        }

        private void DisableCards()
        {
            for (int i = 0; i < 2; i++)
            {
                cards
                .OfType<Card>()
                .ToList()
                .FirstOrDefault(c => c.Index == game.Click[i].IndiceClick)
                .Enabled = false;
            }
        }

        #endregion
    }
}
