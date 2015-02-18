using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;

using IDrawable = RouteDefense.Interfaces.IDrawable;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.UI
{
    public class GUIElement : IDrawable, IUpdateable
    {
        private Texture2D guiTexture;

        public GUIElement(Rectangle rectangle, Texture2D texture)
        {
            this.guiTexture = texture;
            this.Rectangle = rectangle;
        }

        public GUIElement(Rectangle rectangle, Texture2D texture, Action onClick)
            : this(rectangle, texture)
        {
            this.OnClick = onClick;
        }

        public Rectangle Rectangle { get; private set; }

        public Action OnClick { get; set; }

        public void Update()
        {
            if (this.Rectangle.Contains(InputHandler.MouseState.Position)
                && InputHandler.MouseState.LeftButton == ButtonState.Pressed)
            {
                this.OnClick();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.guiTexture, this.Rectangle, Color.DeepSkyBlue); 
        }
    }
}
