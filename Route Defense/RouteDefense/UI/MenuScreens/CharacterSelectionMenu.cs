using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.Models.GameScreens;
using RouteDefense.UI;

namespace RouteDefense.UI.MenuScreens
{
    public class CharacterSelectionMenu : Menu
    {
        private List<GUIElement> characters;

        public CharacterSelectionMenu()
        {
            characters = new List<GUIElement>()
            {
                new GUIElement(new Rectangle(263, 30, 305, 42), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/character select.png"),
                    delegate() {}),

                new GUIElement(new Rectangle(35, 90, 206, 356), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/charTest.png"),
                    delegate() { SubGameEngine.gameState = GameState.Game; })
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement item in characters)
            {
                item.Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            foreach (GUIElement item in characters)
            {
                item.Update();
            }
            HandleInput(new InputHandler());
        }

        public override void HandleInput(InputHandler inputHandler)
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                SubGameEngine.menuState = MenuState.MainMenu;
            }
        }
    }
}
