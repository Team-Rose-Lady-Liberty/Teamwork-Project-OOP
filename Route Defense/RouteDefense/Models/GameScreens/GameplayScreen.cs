using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.UI;
using IDrawable = RouteDefense.Interfaces.IDrawable;

namespace RouteDefense.Models.GameScreens
{
    public class GameplayScreen : GameScreen
    {
        private IDrawable[] toDraw;
        private GUIText text = new GUIText(new Rectangle(0, 400, 10,20), null, "test");
        public GameplayScreen(IDrawable[] drawables)
        {
            toDraw = drawables;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawable drawable in this.toDraw)
            {
                drawable.Draw(spriteBatch);
            }
            text.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public override void HandleInput(InputHandler inputHandler)
        {
            
        }

        public void HandleInput()
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                SubGameEngine.gameState = GameState.Menu;
                SubGameEngine.menuState = MenuState.MainMenu;
            }
        }
    }
}
