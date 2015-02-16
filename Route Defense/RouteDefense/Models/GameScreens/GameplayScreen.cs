using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.Interfaces;

namespace RouteDefense.Models.GameScreens
{
    public class GameplayScreen : GameScreen
    {
        private IDrawable[] toDraw;

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
        }

        public override void Update()
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
