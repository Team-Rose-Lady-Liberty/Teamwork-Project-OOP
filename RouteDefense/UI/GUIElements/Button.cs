using System;
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
            if (this.Rectangle.Contains(InputHandler.CurrentMouseState.Position))
            {
                if (InputHandler.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && InputHandler.OldMouseState.LeftButton == ButtonState.Released)
                {
                    this.OnClick();
                    if (this.IsClicked == false)
                    {
                        this.IsClicked = true;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, this.Rectangle, Color.White);
            spriteBatch.DrawString(GameEngine.GameFont, this.text,textPosition, Color.Black);
        }
    }
}
