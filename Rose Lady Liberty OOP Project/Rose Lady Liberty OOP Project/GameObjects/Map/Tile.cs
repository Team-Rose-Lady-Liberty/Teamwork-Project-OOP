namespace RoseLadyLibertyOOPProject.GameObjects.Map
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Tile : GameObject
    {
        private Texture2D tileTexture;

        public Tile(string id, int x, int y, Texture2D texture)
            : base(id, x, y)
        {
            this.IsPath = false;
            this.TileTexture = texture;
        }

        public Texture2D TileTexture 
        {
            get { return this.tileTexture; } 
            set
            {
                this.tileTexture = value;
                this.Width = this.TileTexture.Width;
                this.Height = this.TileTexture.Height;
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public bool IsPath { get; set; }

        public void Draw(SpriteBatch spriteBatch, int drawWidth, int drawHeight)
        {
           
            spriteBatch.Draw(this.tileTexture, new Rectangle(this.X,this.Y,drawWidth,drawHeight), new Rectangle(0,0,this.Width,this.Height),Color.White);
            
        }
    }
}
