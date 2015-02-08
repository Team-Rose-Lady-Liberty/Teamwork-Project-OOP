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

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            spriteBatch.Begin();
            spriteBatch.Draw(temp, this.Rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
