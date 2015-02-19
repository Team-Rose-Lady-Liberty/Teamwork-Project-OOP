using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core;

namespace RouteDefense.UI
{
    public class GUIText : GUIElement
    {
        private string text;

        public GUIText(Rectangle rectangle, Texture2D texture) 
            : base(rectangle, texture)
        {
        }

        public GUIText(Rectangle rectangle, Texture2D texture, string textToDraw)
            :this(rectangle,null)
        {
            this.text = textToDraw;
        }

        public GUIText(Rectangle rectangle, Texture2D texture, Action onClick) : base(rectangle, texture, onClick)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SubGameEngine.ContentManager.Load<SpriteFont>("Fonts\\TestFont"),
                this.text, new Vector2(Rectangle.X, Rectangle.Y), Color.White);
        }
    }
}
