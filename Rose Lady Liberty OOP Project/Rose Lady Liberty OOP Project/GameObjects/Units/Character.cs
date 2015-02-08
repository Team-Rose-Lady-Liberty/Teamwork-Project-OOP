namespace RoseLadyLibertyOOPProject.GameObjects
{
    using Microsoft.Xna.Framework;
    using Enumerations;

    public abstract class Character : Unit
    {
        public Character(string id, Rectangle rectangle, int health, int attack, int defense)
            : base(id, rectangle, health, attack, defense)
        {

        }

        public virtual void Move(MoveDirection direction)
        {
            int speedX = 0;
            int speedY = 0;

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
                default:
                    break;
            }
            this.Rectangle = new Rectangle(this.Rectangle.X + speedX, this.Rectangle.Y + speedY, this.Rectangle.Width, this.Rectangle.Height);
        }
    }
}
