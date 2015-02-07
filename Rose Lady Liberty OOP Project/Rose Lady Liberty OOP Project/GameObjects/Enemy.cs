namespace RoseLadyLibertyOOPProject.GameObjects
{
    using System;

    public abstract class Enemy : GameObject
    {
        private int range;

        public Enemy(string id, int x, int y)
            : base(id, x, y)
        {

        }

        public int Range
        {
            get { return this.range; }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("The range cannot be negative number!");
                }
                else
                {
                    this.range = value;
                }
            }
        }
    }
}
