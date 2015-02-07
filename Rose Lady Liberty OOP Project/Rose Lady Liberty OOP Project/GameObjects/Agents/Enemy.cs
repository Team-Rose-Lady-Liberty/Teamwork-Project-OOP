namespace RoseLadyLibertyOOPProject.GameObjects
{
    using System;

    public abstract class Enemy : Agent
    {
        public Enemy(string id, int x, int y, int health, int attack, int defense)
            : base(id, x, y, health, attack, defense)
        {

        }
    }
}
