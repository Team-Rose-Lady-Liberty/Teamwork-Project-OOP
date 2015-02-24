using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RouteDefense.UI.GUIElements
{
    public class Label : GUIElement
    {
        private string text;

        public Label(Rectangle rectangle, string text)
            : base(rectangle)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameEngine.GameFont,
                text, new Vector2(this.Rectangle.X, this.Rectangle.Y), Color.White);
        }
    }
}
