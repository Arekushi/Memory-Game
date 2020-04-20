using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Timers;

namespace prjJogoMemoria {
    public class CreateAnimation {

        private readonly System.Timers.Timer aTimer;
        private readonly PictureBox ptb;
        private readonly Bitmap[] frames;
        private int indexFrame = 0;

        public CreateAnimation(PictureBox ptb, int intFrames, string path, int velocidade) {
            aTimer = new System.Timers.Timer(1000 / velocidade) {
                AutoReset = true
            };

            frames = new Bitmap[intFrames];
            this.ptb = ptb;

            for (int i = 0; i < intFrames; i++) {
                frames[i] = new Bitmap($"{path}{i + 1}.png");
            }

            this.ptb.Image = frames[indexFrame];
        }

        public void StartAnimationLoop() {
            aTimer.Start();

            aTimer.Elapsed += (o, e) => {
                indexFrame = ++indexFrame % frames.Length;
                ptb.Image = frames[indexFrame];
            };
        }

        public void StartAnimation() {
            aTimer.Start();

            aTimer.Elapsed += (o, e) => {
                indexFrame = ++indexFrame % frames.Length;
                ptb.Image = frames[indexFrame];

                if (indexFrame == (frames.Length - 1)) {
                    StopAnimation();
                }
            };
        }

        public void StopAnimation() {
            aTimer.Stop();
        }

    }
}
