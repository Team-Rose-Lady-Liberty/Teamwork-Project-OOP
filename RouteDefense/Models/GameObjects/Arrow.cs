using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense.Models.GameObjects
{
    public class Arrow : GameObject
    {
        public Point Velocity;
        public Texture2D texture;

        public Arrow(Rectangle rectangle, Point Velocity, Texture2D texture)
            : base(rectangle)
        {
            this.Velocity = Velocity;
            this.texture = texture;
        }

        public void Update()
        {
            Rectangle = new Rectangle(Rectangle.X + Velocity.X, Rectangle.Y + Velocity.Y, Rectangle.Width, Rectangle.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, Rectangle, Color.White);
        }
    }
}
