using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoseLadyLibertyOOPProject.UI
{
   
    public class GUIElement
    {
        public OnClick onClickHandler;

        public GUIElement(Rectangle rectangle, OnClick action)
        {
            this.Rectangle = rectangle;
            this.onClickHandler = action;
        }

        public Rectangle Rectangle { get; private set; }

        public void Update()
        {
            if (this.Rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.onClickHandler();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture2D = new Texture2D(spriteBatch.GraphicsDevice, this.Rectangle.Width, this.Rectangle.Height);
            texture2D.SetData(new[] { Color.White });
            
            spriteBatch.Draw(texture2D, this.Rectangle, Color.White);
        }
    }
}
