using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoseLadyLibertyOOPProject.Enumerations;
using RoseLadyLibertyOOPProject.UI;

namespace RoseLadyLibertyOOPProject.Core.GameScreens
{
    public class MainMenu : GameScreen
    {
        private List<MenuItem> menuItems;

        public MainMenu()
        {
            menuItems = new List<MenuItem>()
            {
                new MenuItem(new Rectangle(100, 0, 70, 30), delegate(){GameEngine.gameState = GameState.Game;})
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in menuItems)
            {
                item.Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            foreach (MenuItem item in menuItems)
            {
                item.Update();
            }
        }
    }
}
