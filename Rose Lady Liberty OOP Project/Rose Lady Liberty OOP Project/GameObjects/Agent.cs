namespace RoseLadyLibertyOOPProject.GameObjects
{
    using Interfaces;
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Agent : GameObject, IMovable, IDrawable
    {
        private int range;

        public Agent(string id, int x, int y, int health, int attack, int defense)
            : base(id, x, y)
        {
            this.IsAlive = true;
            this.Health = health;
            this.Attack = attack;
            this.Defense = defense;
        }

        public int Health { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public bool IsAlive { get; set; }

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

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
