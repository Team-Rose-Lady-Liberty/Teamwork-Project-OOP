using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense
{
    public class Animation
    {
        Texture2D spriteSheet;
        // the elapsed amount of time the frame has been shown for
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

        public Animation(Texture2D spriteSheet, int startPositionX, int startPositionY,
            int frameWidth, int frameHeight, int totalFrames, float frameTime)
        {
            this.spriteSheet = spriteSheet;
            this.startPositionX = startPositionX;
            this.startPositionY = startPositionY;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.totalFrames = totalFrames;
            this.frameTime = frameTime;

            frameIndex = 1;

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
                        frameIndex = 1;
                    }
                    this.drawRectangle = new Rectangle(startPositionX + frameWidth*frameIndex, startPositionY,
                        frameWidth, frameHeight);
                    time = 0f;
                }
            }
        }

        public void Stop()
        {
            this.frameIndex = 1;
        }

        public void Start()
        {
            this.IsLooping = true;
        }

        public void StayAtFirst()
        {
            frameIndex = 1;
            IsLooping = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight),
                drawRectangle, Color.White);
        }
    }
}
