using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    public class Enemy : Unit
    {
        private int currentPathNode;
        private Texture2D texture;

        private Dictionary<MoveDirection, Animation> animations = new Dictionary<MoveDirection, Animation>(); 

        public Enemy(string id, Rectangle rectangle) 
            : base(id, rectangle)
        {
            this.Health = 100;
            currentPathNode = 0;
            this.texture = SubGameEngine.ContentManager.Load<Texture2D>("EnemiesSprites\\EnemyBaby0.png");
            animations.Add(MoveDirection.Right, new Animation(1, 710, 64, 60, 9, 0.1f));
            animations.Add(MoveDirection.Up, new Animation(1, 645, 64, 60, 9, 0.1f));
            animations.Add(MoveDirection.Down, new Animation(1, 520, 64, 60, 9, 0.1f));
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
                AtFinish = true;
            }
        }

        public bool AtFinish { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Rectangle, this.animations[direction].drawRectangle, Color.White);
        }
    }
}
