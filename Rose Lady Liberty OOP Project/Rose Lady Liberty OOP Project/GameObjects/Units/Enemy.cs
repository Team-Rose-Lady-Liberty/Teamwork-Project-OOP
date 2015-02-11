namespace RoseLadyLibertyOOPProject.GameObjects.Units
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using GameObjects.Map;
    using Enumerations;

    public abstract class Enemy : Unit
    {
        private int currentPathNode = 0;
        public Enemy(string id, Rectangle rectangle, int health, int attack, int defense)
            : base(id, rectangle, health, attack, defense)
        {
            AtFinish = false;
        }

        public void Update(ref Tile[] path)
        {
            if(currentPathNode + 1 < path.Length)
            {
                if (this.Rectangle.X + this.Rectangle.Width < path[currentPathNode + 1].Rectangle.X + path[currentPathNode + 1].Rectangle.Width
                    && this.Rectangle.Y == path[currentPathNode + 1].Rectangle.Y)
                {
                    this.Rectangle = new Rectangle(this.Rectangle.X + 1, this.Rectangle.Y
                        , this.Rectangle.Width, this.Rectangle.Height);
                    if (this.Rectangle.X >= path[currentPathNode + 1].Rectangle.X)
                    {
                        this.Rectangle = path[currentPathNode + 1].Rectangle;
                        currentPathNode++;
                    }
                }
                else if (this.Rectangle.X == path[currentPathNode + 1].Rectangle.X
                    && this.Rectangle.Y > path[currentPathNode + 1].Rectangle.Y)
                {
                    this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y - 1
                        , this.Rectangle.Width, this.Rectangle.Height);
                    if (this.Rectangle.Y <= path[currentPathNode + 1].Rectangle.Y)
                    {
                        this.Rectangle = path[currentPathNode + 1].Rectangle;
                        currentPathNode++;
                    }
                }
                else if (this.Rectangle.X == path[currentPathNode + 1].Rectangle.X
                    && this.Rectangle.Y < path[currentPathNode + 1].Rectangle.Y)
                {
                    this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y + 1
                        , this.Rectangle.Width, this.Rectangle.Height);
                    if (this.Rectangle.Y >= path[currentPathNode + 1].Rectangle.Y)
                    {
                        this.Rectangle = path[currentPathNode + 1].Rectangle;
                        currentPathNode++;
                    }
                }
            }
            else
            {
                AtFinish = true;
            }
        }

        public bool AtFinish { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            spriteBatch.Draw(temp, this.Rectangle, Color.White);
        }
    }
}
