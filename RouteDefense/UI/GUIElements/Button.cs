using System;
using System.Runtime.Remoting.Contexts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RouteDefense.Core;

namespace RouteDefense.UI.GUIElements
{
    public class Button : GUIElement
    {
        public Action OnClick;

        public bool IsClicked;

        private string text;

        private Vector2 textPosition;

        private Texture2D buttonTexture;

        public Button(Rectangle rectangle, Texture2D buttonTexture, string text, Action onClickAction)
            : base(rectangle)
        {
            this.buttonTexture = buttonTexture;
            this.OnClick = new Action(onClickAction);
            this.text = text;

            textPosition = new Vector2(Rectangle.X + (Rectangle.Width - GameEngine.GameFont.MeasureString(text).X) / 2,
                Rectangle.Y + (Rectangle.Height - GameEngine.GameFont.MeasureString(text).Y) / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Rectangle.Contains(InputHandler.MouseState.Position))
            {
                if (GameEngine.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && GameEngine.OldMouseState.LeftButton == ButtonState.Released)
                {
                    if (this.IsClicked == false)
                    {
                        this.OnClick();
                        this.IsClicked = true;
                    }
                }
            }
            if (InputHandler.MouseState.LeftButton == ButtonState.Released)
            {
                this.IsClicked = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, this.Rectangle, Color.White);
            spriteBatch.DrawString(GameEngine.GameFont, this.text,textPosition, Color.Black);
        }
    }
}
