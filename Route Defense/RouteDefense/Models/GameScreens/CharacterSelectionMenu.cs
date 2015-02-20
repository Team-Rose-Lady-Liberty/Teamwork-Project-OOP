using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.UI;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.Models.GameScreens
{
    public class CharacterSelectionMenu : GameScreen
    {
        private List<GUIElement> characters;

        public CharacterSelectionMenu()
        {
            characters = new List<GUIElement>()
            {
                new Button(new Rectangle(263, 30, 305, 42), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/character select.png"),
                    delegate() {}),

                new Button(new Rectangle(35, 90, 206, 356), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/charTest.png"),
                    delegate() { SubGameEngine.CurrentGameState = GameState.Game; })
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement item in characters)
            {
                item.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GUIElement item in characters)
            {
                item.Update(gameTime);
            }
            HandleInput(new InputHandler());
        }

        public override void HandleInput(InputHandler inputHandler)
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                SubGameEngine.CurrentGameState = GameState.MainMenu;
            }
        }
    }
}
