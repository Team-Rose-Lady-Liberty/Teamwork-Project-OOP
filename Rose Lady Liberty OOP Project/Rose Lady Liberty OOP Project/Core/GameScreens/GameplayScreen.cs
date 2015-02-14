using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoseLadyLibertyOOPProject.Enumerations;
using RoseLadyLibertyOOPProject.GameObjects;
using RoseLadyLibertyOOPProject.GameObjects.Map;
using RoseLadyLibertyOOPProject.UI;

namespace RoseLadyLibertyOOPProject.Core.GameScreens
{
    public class GameplayScreen : GameScreen
    {
        private Map mapToDraw;
        private Character characterToDraw;
        private List<GUIElement> guiElements;

        public GameplayScreen(Map theMap, Character character)
        {
            this.mapToDraw = theMap;
            this.characterToDraw = character;
            this.guiElements = new List<GUIElement>();
        }

        public override void Update()
        {
            HandleInput();
            foreach (GUIElement element in guiElements)
            {
                element.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.mapToDraw.Draw(spriteBatch);
            this.characterToDraw.Draw(spriteBatch);
            foreach (GUIElement element in guiElements)
            {
                element.Draw(spriteBatch);
            }
        }

        private void HandleInput()
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                GameEngine.gameState = GameState.Menu;
                GameEngine.menuState = MenuState.MainMenu;
            }
        }
    }
}
