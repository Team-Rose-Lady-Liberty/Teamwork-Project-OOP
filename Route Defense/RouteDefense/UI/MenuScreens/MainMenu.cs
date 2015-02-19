using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RouteDefense.Core;
using RouteDefense.Enumerations;
using RouteDefense.Models.GameScreens;
using RouteDefense.UI;

namespace RouteDefense.UI.MenuScreens
{
    public class MainMenu : Menu
    {
        private List<GUIElement> elements;

        public MainMenu()
        {
            elements = new List<GUIElement>()
            {
                new GUIElement(new Rectangle(366, 178, 100, 24), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/new game.png"),
                    delegate(){SubGameEngine.menuState = MenuState.CharacterSelectionMenu;}),

                new GUIElement(new Rectangle(357, 228, 118, 24), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/game story.png"),
                    delegate(){SubGameEngine.gameState = GameState.Game;}),

                new GUIElement(new Rectangle(364, 278, 104, 29), SubGameEngine.ContentManager.Load<Texture2D>("Menu Items/quit game.png"),
                    delegate(){}),
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement item in elements)
            {
                item.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GUIElement item in elements)
            {
                item.Update(gameTime);
            }
        }
    }
}
