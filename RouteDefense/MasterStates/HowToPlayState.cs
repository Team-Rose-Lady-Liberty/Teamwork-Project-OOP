using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;
using RouteDefense.UI.GUIElements;


namespace RouteDefense.MasterStates
{
    public class HowToPlayState : MasterState
    {
        private Picture controls;

        public HowToPlayState(GameEngine context)
        {
            this.Context = context;
            controls = new Picture(new Rectangle(0, 0, 1024, 672),
                Context.Content.Load<Texture2D>("Menu Items/background how to play.png"));
        }

        public override IMasterState Update(GameTime gameTime)
        {
            return null;
        }

        public override IMasterState HandleInput()
        {
            if (InputHandler.IsClicked(Keys.Escape))
            {
                return new MainMenuState(Context);
            }

            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            controls.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
