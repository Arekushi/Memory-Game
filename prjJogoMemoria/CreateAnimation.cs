using System.Drawing;
using System.Windows.Forms;

namespace prjJogoMemoria
{
    public class CreateAnimation
    {
        #region Variables

        private readonly System.Timers.Timer aTimer;
        private readonly PictureBox ptb;
        private readonly Bitmap[] frames;
        private int indexFrame = 0;

        #endregion

        #region Methods

        public CreateAnimation(PictureBox ptb, int intFrames, string path, int velocity)
        {
            aTimer = new System.Timers.Timer(1000 / velocity)
            {
                AutoReset = true
            };

            frames = new Bitmap[intFrames];
            this.ptb = ptb;

            for (int i = 0; i < intFrames; i++)
            {
                frames[i] = new Bitmap($"{path}{i + 1}.png");
            }

            this.ptb.Image = frames[indexFrame];
        }

        public void StartAnimationLoop()
        {
            aTimer.Start();

            aTimer.Elapsed += (o, e) =>
            {
                indexFrame = ++indexFrame % frames.Length;
                ptb.Image = frames[indexFrame];
            };
        }

        public void StartAnimation()
        {
            aTimer.Start();

            aTimer.Elapsed += (o, e) =>
            {
                indexFrame = ++indexFrame % frames.Length;
                ptb.Image = frames[indexFrame];

                if (indexFrame == (frames.Length - 1))
                {
                    StopAnimation();
                }
            };
        }

        public void StopAnimation()
        {
            aTimer.Stop();
        }

        #endregion
    }
}
