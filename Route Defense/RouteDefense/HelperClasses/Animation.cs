using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense
{
    public class Animation
    {
        Texture2D spriteSheet;
        // the elapsed amount of time the frame has been shown for
        float time;
        // duration of time to show each frame
        private float frameTime;
        // an index of the current frame being shown
        private int frameIndex;
        // total number of frames in our spritesheet
        private int totalFrames;
        private int startPositionX;
        private int startPositionY;
        // define the size of our animation frame
        private int frameWidth;
        private int frameHeight;
        

        private Rectangle drawRectangle;

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
        }

        public void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                frameIndex++;
                if (frameIndex >= totalFrames)
                {
                    frameIndex = 1;
                }
                this.drawRectangle = new Rectangle(startPositionX + frameWidth * frameIndex, startPositionY,
                    frameWidth, frameHeight);
                time = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight),
                drawRectangle, Color.White);
        }
    }
}
