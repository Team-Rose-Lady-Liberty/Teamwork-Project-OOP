#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoseLadyLibertyOOPProject.Core;
#endregion

namespace RoseLadyLibertyOOPProject
{
    public class TheGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameEngine engine;

        public TheGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 832;
            this.IsMouseVisible = true;
           
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.engine = new GameEngine(this.Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            this.engine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.engine.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
