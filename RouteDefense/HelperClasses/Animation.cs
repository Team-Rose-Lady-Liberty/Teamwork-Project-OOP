using Microsoft.Xna.Framework;

namespace RouteDefense
{
    public class Animation
    {
        float time;

        private float frameTime;

        private int frameIndex;

        private int totalFrames;
        // stating position of the first frame in the spritesheet
        private int startPositionX;
        private int startPositionY;

        private int frameWidth;
        private int frameHeight;

        public Rectangle drawRectangle { get; private set; }

        public bool IsLooping { get; set; }

        public Animation(int startPositionX, int startPositionY,
            int frameWidth, int frameHeight, int totalFrames, float frameTime)
        {
            this.startPositionX = startPositionX;
            this.startPositionY = startPositionY;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.totalFrames = totalFrames;
            this.frameTime = frameTime;

            frameIndex = 0;

            this.drawRectangle = new Rectangle(startPositionX + frameWidth * frameIndex, startPositionY,
                        frameWidth, frameHeight);
            IsLooping = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsLooping)
            {
                time += (float) gameTime.ElapsedGameTime.TotalSeconds;
                while (time > frameTime)
                {
                    frameIndex++;
                    if (frameIndex >= totalFrames)
                    {
                        frameIndex = 0;
                        IsFinished = true;
                    }
                    this.drawRectangle = new Rectangle(startPositionX + frameWidth * frameIndex, startPositionY,
                        frameWidth, frameHeight);
                    time = 0f;
                }
            }
        }

        public void Reset()
        {
            this.frameIndex = 0;
            this.drawRectangle = new Rectangle(startPositionX + frameWidth * frameIndex, startPositionY,
                        frameWidth, frameHeight);
        }

        public void Stop()
        {
            this.frameIndex = 0;
            IsLooping = false;
            this.drawRectangle = new Rectangle(startPositionX + frameWidth * frameIndex, startPositionY,
                        frameWidth, frameHeight);
        }

        public bool IsFinished { get; set; }

        public void Start()
        {
            IsLooping = true;
        }
    }
}
