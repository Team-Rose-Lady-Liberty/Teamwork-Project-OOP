using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RoseLadyLibertyOOPProject.Core.GameScreens;

namespace RoseLadyLibertyOOPProject.Core
{
    using GameObjects.Map;
    using Enumerations;

    public class GameEngine
    {
        public static GameState gameState;
        public static MenuState menuState;

        private GameLogic gameCoreLogic;
        private GameplayScreen gameplayScreen;
        private Dictionary<MenuState, GameScreen> menuScreens;
 
        public GameEngine(ContentManager contentManager)
        {
            gameState = GameState.Menu;
            menuState = MenuState.MainMenu;

            this.gameCoreLogic = new GameLogic(contentManager);
            this.gameplayScreen = new GameplayScreen(this.gameCoreLogic.theMap, this.gameCoreLogic.theCharacter);

            this.menuScreens = new Dictionary<MenuState, GameScreen>();
            this.menuScreens.Add(MenuState.MainMenu, new MainMenu());
            this.menuScreens.Add(MenuState.CharacterSelectionMenu, new CharacterSelectionMenu());
            this.menuScreens.Add(MenuState.OptionsMenu, new OptionsMenu());
        }

        public void Update()
        {
            switch (gameState)
            {
                case GameState.Game:
                    this.gameCoreLogic.Update();
                    this.gameplayScreen.Update();
                    break;
                case GameState.Menu:
                    menuScreens[menuState].Update();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Game:
                    this.gameplayScreen.Draw(spriteBatch);
                    break;
                case GameState.Menu:
                    menuScreens[menuState].Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
        }
    }
}
