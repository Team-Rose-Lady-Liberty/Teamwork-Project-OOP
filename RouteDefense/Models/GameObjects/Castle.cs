using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense.Models.GameObjects
{
    public class Castle : GameObject
    {
        public const int DefaultHealth = 100;
        public const int DefaultPriceToUpgrade = 30;

        public int castleHealth;
        private Texture2D texture;

        public int Level;

        public int PriceToUpgrade;

        public Castle(Rectangle rectangle, Texture2D texture)
            : base(rectangle)
        {
            castleHealth = DefaultHealth;
            this.texture = texture;
            Level = 1;
            PriceToUpgrade = DefaultPriceToUpgrade;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
