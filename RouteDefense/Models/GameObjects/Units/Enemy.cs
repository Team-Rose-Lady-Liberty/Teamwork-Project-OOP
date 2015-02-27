using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    public class Enemy : Unit
    {
        private int currentPathNode;

        private Texture2D texture;

        private Dictionary<MoveDirection, Animation> animations = new Dictionary<MoveDirection, Animation>();

        public int Gold { get; private set; }

        private const float animationFrameRate = 0.1f;

        public Enemy(Rectangle rectangle, Texture2D texture, int health, int gold, int attack, int speed) 
            : base(rectangle)
        {
            currentPathNode = 0;

            this.Health = health;
            this.Gold = gold;
            this.Attack = attack;
            this.Speed = speed;

            this.texture = texture;

            IsAlive = true;

            animations.Add(MoveDirection.Right, new Animation(0, 704, Constants.FrameWidth, Constants.FrameHeight, 9, animationFrameRate));
            animations.Add(MoveDirection.Up, new Animation(0, 640, Constants.FrameWidth, Constants.FrameHeight, 9, animationFrameRate));
            animations.Add(MoveDirection.Down, new Animation(0, 512, Constants.FrameWidth, Constants.FrameHeight, 9, animationFrameRate));
        }

        private MoveDirection direction;

        public void Update(GameTime gameTime, ref Tile[] path)
        {
            this.animations[direction].Update(gameTime);
            if (currentPathNode + 1 < path.Length)
            {
                if (this.Rectangle.X + this.Rectangle.Width < path[currentPathNode + 1].Rectangle.X + path[currentPathNode + 1].Rectangle.Width
                    && this.Rectangle.Y == path[currentPathNode + 1].Rectangle.Y)
                {
                    direction = MoveDirection.Right;
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
                    direction = MoveDirection.Down;
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
                    direction = MoveDirection.Up;
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
                IsAlive = true;
            }
        }

        public bool IsAlive { get; private set; }

        public void Kill()
        {
            this.IsAlive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Rectangle, this.animations[direction].drawRectangle, Color.White);
        }
    }
}
