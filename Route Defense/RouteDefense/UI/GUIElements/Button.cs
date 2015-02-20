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

        public GUIElementType ButtonType { get; private set; }

        private string text;

        private Vector2 textPosition;

        private Texture2D buttonTexture;

        private Color color;

        public Button(Rectangle rectangle, Texture2D buttonTexture, Action onClickAction)
            : base(rectangle)
        {
            this.buttonTexture = buttonTexture;
            this.OnClick = new Action(onClickAction);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Rectangle.Contains(InputHandler.MouseState.Position))
            {
                if (InputHandler.MouseState.LeftButton == ButtonState.Pressed)
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
        }
    }
}
