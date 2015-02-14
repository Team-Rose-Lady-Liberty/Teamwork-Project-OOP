namespace RoseLadyLibertyOOPProject.GameObjects.Units.Characters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class SampleCharacter : Character
    {
        public SampleCharacter(string id, Rectangle rectangle, int health, int attack, int defense)
            : base(id, rectangle, health, attack, defense)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            //spriteBatch.Begin();
            spriteBatch.Draw(temp, this.Rectangle, Color.Black);
            //spriteBatch.End();
        }
    }
}
