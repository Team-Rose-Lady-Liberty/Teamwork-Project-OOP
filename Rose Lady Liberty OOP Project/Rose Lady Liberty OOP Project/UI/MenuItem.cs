using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoseLadyLibertyOOPProject.UI
{
    public delegate void OnClick();

    public class MenuItem
    {
        private OnClick onClickListener;

        public MenuItem(Rectangle rectangle, OnClick actionOnClick)
        {
            this.Rectangle = rectangle;
            this.onClickListener = new OnClick(actionOnClick);
        }

        public Rectangle Rectangle{ get; private set; }

        public void Update()
        {
            if (this.Rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.onClickListener();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture2D = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture2D.SetData(new[] { Color.Black });
            
            spriteBatch.Draw(texture2D, this.Rectangle, Color.White);
        }
    }
}
