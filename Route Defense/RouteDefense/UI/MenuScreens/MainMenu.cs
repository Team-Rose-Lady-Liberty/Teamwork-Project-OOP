using System.Collections.Generic;

using Microsoft.Xna.Framework;
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
                new GUIElement(new Rectangle(300, 0, 70, 30), SubGameEngine.ContentManager.Load<Texture2D>("Terrain\\water.png"),
                    delegate(){SubGameEngine.gameState = GameState.Game;})
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
