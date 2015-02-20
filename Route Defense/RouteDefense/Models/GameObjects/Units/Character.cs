using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    public class Character : Unit
    {
        protected Dictionary<MoveDirection, Animation> movingAnimations;
        private Texture2D texture;

        public bool IsMoving;

        public Rectangle ActualRectangle;

        public Character(string id, Rectangle rectangle)
            : base(id, rectangle)
        {
            IsMoving = false;
            AnimState = MoveDirection.Down;
            texture = SubGameEngine.ContentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png");
            movingAnimations = new Dictionary<MoveDirection, Animation>();
            movingAnimations.Add(MoveDirection.Down, new Animation(1, 645, 64, 62, 9, 0.1f));
            movingAnimations.Add(MoveDirection.Up, new Animation(1, 517, 64, 62, 9, 0.1f));
            movingAnimations.Add(MoveDirection.Left, new Animation(1, 582, 64, 62, 9, 0.1f));
            movingAnimations.Add(MoveDirection.Right, new Animation(1, 710, 64, 62, 9, 0.1f));

            this.Range = 32;

            this.RangeRectangle = new Rectangle(this.Rectangle.X - Range, this.Rectangle.Y - Range,
                this.Rectangle.Width + 2 * this.Range, this.Rectangle.Height + 2 * Range);

            ActualRectangle = new Rectangle(this.Rectangle.X + 8, this.Rectangle.Y, this.Rectangle.Width - 16, this.Rectangle.Height);
        }

        public MoveDirection MoveState { get; private set; }

        public MoveDirection AnimState { get; set; }

        public CharacterState CharacterState { get; private set; }

        public void Move(MoveDirection direction)
        {
            int speedX = 0;
            int speedY = 0;

            this.MoveState = direction;

            switch (direction)
            {
                case MoveDirection.Up:
                    speedY = -this.Speed;
                    break;
                case MoveDirection.Down:
                    speedY = this.Speed;
                    break;
                case MoveDirection.Right:
                    speedX = this.Speed;
                    break;
                case MoveDirection.Left:
                    speedX = -this.Speed;
                    break;
            }
            this.Rectangle = new Rectangle(this.Rectangle.X + speedX, this.Rectangle.Y + speedY,
                this.Rectangle.Width, this.Rectangle.Height);
            this.RangeRectangle = new Rectangle(this.Rectangle.X - Range, this.Rectangle.Y - Range,
                this.RangeRectangle.Width, this.RangeRectangle.Height);
            ActualRectangle = new Rectangle(this.Rectangle.X + 16, this.Rectangle.Y, this.Rectangle.Width - 32, this.Rectangle.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if(IsMoving == true && movingAnimations[AnimState].IsLooping == false)
                movingAnimations[AnimState].Start();
            else if(IsMoving == false)
                movingAnimations[AnimState].Stop();
            this.movingAnimations[AnimState].Update(gameTime);
        }

        public List<Enemy> GetTargets(IEnumerable<Enemy> enemies)
        {
            return (from enemy in enemies
                    where this.RangeRectangle.Intersects(enemy.Rectangle)
                select enemy).ToList();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

           // spriteBatch.Draw(temp, this.ActualRectangle, Color.White);
            spriteBatch.Draw(texture, this.Rectangle, this.movingAnimations[AnimState].drawRectangle, Color.White);
        }
    }
}
