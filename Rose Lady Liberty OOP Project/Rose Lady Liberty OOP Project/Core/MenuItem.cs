namespace RoseLadyLibertyOOPProject.Core
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public delegate void EventHandler(); 

    public class MenuItem
    {
        private Rectangle rectangle;
        public Color color;

        public event EventHandler clickEvent;
        public event EventHandler releaseEvent;

        public MenuItem(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
            color = Color.Gold;
            clickEvent += new EventHandler(OnPress);
            releaseEvent += new EventHandler(OnRelease);
        }

        public void OnPress()
        {
            this.color = Color.Aqua;
        }

        public void OnRelease()
        {
            this.color = Color.Green;
        }

        public void Update()
        {
            if (this.Rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.clickEvent();
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                this.releaseEvent();
            }
        }

        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            private set { this.rectangle = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            spriteBatch.Draw(temp,this.Rectangle, this.color);

        }
    }
}
