namespace RoseLadyLibertyOOPProject.GameObjects
{
    using System;
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Character : Agent
    {
        public Character(string id, int x, int y, int health, int attack, int defense)
            : base(id, x, y, health, attack, defense)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
