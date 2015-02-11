namespace RoseLadyLibertyOOPProject.GameObjects.Units.Enemies
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SampleEnemy : Enemy
    {
        public SampleEnemy(string id, Rectangle rectangle, int health, int attack, int defense)
            : base(id, rectangle, health, attack, defense)
        {
            
        }
    }
}
