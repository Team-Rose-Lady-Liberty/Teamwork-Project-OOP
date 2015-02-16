using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense
{
    public class Animation : IUpdateable
    {
        private Texture2D spriteSheet;
        private Rectangle sourceRectangle;
        private Rectangle drawRectangle;
        private int spritesCount;
        private int currentSprite;
        private Vector2 spriteSize;
        private Vector2 startingPoint;

        private int frameRate;

        private int counterRate;

        public Animation(Texture2D texture, int spritesCount, 
            Vector2 spriteSize, Vector2 startingPoint, int frameRate, int x, int y)
        {
            this.spriteSheet = texture;
            this.spritesCount = spritesCount;
            this.spriteSize = spriteSize;
            this.startingPoint = startingPoint;
            this.frameRate = frameRate;

            this.counterRate = 0;
            this.drawRectangle = new Rectangle((int)this.startingPoint.X, (int)this.startingPoint.Y,
                (int)this.spriteSize.X, (int)this.spriteSize.Y);
            this.sourceRectangle = new Rectangle(x, y, (int)this.spriteSize.X, (int)this.spriteSize.Y); 
        }

        public void Update()
        {
            this.counterRate++;
            if (counterRate >= this.frameRate)
            {
                this.counterRate = 0;
                this.drawRectangle = new Rectangle(this.drawRectangle.X + this.drawRectangle.Width + 2,
                    (int)this.startingPoint.Y,
                    this.drawRectangle.Width, this.drawRectangle.Height);
                if (this.drawRectangle.X >= (this.startingPoint.X + this.drawRectangle.Width)*this.spritesCount)
                {
                    this.drawRectangle = new Rectangle((int)this.startingPoint.X, (int)this.startingPoint.Y,
                    this.drawRectangle.Width, this.drawRectangle.Height);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.spriteSheet, this.sourceRectangle, this.drawRectangle, Color.White);
        }
    }
}
