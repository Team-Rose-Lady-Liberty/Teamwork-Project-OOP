using System.Collections.Generic;
using RoseLadyLibertyOOPProject.GameObjects.Units;

namespace RoseLadyLibertyOOPProject.GameObjects
{
    using Microsoft.Xna.Framework;
    using Enumerations;

    public abstract class Character : Unit
    {
        private Color color;
        public Character(string id, Rectangle rectangle, int health, int attack, int defense)
            : base(id, rectangle, health, attack, defense)
        {
            color = Color.Black;
        }

        public MoveDirection MoveStatus { get; private set; }

        public virtual void Move(MoveDirection direction)
        {
            int speedX = 0;
            int speedY = 0;
            this.MoveStatus = direction;

            switch (this.MoveStatus)
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
                default:
                    break;
            }
            this.Rectangle = new Rectangle(this.Rectangle.X + speedX, this.Rectangle.Y + speedY,
                this.Rectangle.Width, this.Rectangle.Height);
        }
    }
}
