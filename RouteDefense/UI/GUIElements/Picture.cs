using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RouteDefense.UI.GUIElements
{
    public class Picture : GUIElement
    {
        private Texture2D picture;

        public Picture(Rectangle rectangle, Texture2D picture)
            : base(rectangle)
        {
            this.picture = picture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(picture, this.Rectangle, Color.White);
        }
    }
}
