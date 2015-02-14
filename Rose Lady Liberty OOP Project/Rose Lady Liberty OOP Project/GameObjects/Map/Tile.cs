namespace RoseLadyLibertyOOPProject.GameObjects.Map
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Enumerations;

    public class Tile : GameObject
    {
        private Texture2D tileTexture;

        public Tile(string id, int x, int y, int width, int height, Texture2D texture)
            : base(id, x, y, width, height)
        {
            this.TileType = TileType.Decoration;
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

        public TileType TileType { get; set; }

        public MoveDirection Direction { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tileTexture, this.Rectangle, Color.White); 
        }
    }
}
