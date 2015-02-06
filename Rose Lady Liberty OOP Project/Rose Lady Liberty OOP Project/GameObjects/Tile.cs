namespace RoseLadyLibertyOOPProject.GameObjects
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public class Tile : GameObject, IDrawable
    {
        private Texture2D tileTexture;

        public Tile(string id, int x, int y, Texture2D texture)
            : base(id, x, y)
        {
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

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
