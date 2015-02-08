namespace RoseLadyLibertyOOPProject.GameObjects
{
    using System;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    using Interfaces;

    public abstract class Unit : GameObject, IMovable, Interfaces.IDrawable
    {
        private int range;

        public Unit(string id, Rectangle unitRectangle, int health, int attack, int defense)
            : base(id, unitRectangle)
        {
            this.Health = health;
            this.Attack = attack;
            this.Defense = defense;
        }

        public int Health { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Speed { get; set; }

        public int Range
        {
            get { return this.range; }
            set 
            {
                if(value < 0)
                {
                    throw new ArgumentException("The range of character cannot be negative number!");
                }
                else
                {
                    this.range = value;
                }              
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
