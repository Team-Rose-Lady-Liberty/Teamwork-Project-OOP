using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core;

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
            spriteBatch.DrawString(SubGameEngine.ContentManager.Load<SpriteFont>("Fonts\\TestFont"), 
                text, new Vector2(this.Rectangle.X, this.Rectangle.Y), Color.White);
        }
    }
}
