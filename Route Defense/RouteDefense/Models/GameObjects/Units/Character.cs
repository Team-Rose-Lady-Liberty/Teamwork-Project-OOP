using System;
using Microsoft.Xna.Framework;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    public class Character : Unit
    {
        public Character(string id, Rectangle rectangle)
            : base(id, rectangle)
        {
        }

        public MoveDirection MoveState { get; private set; }

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
        }
    }
}
