using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RouteDefense.Enumerations;
using RouteDefense.Models.GameScreens;
using RouteDefense.UI.MenuScreens;
using IDrawable = RouteDefense.Interfaces.IDrawable;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.Core
{
    public class SubGameEngine : IDrawable, IUpdateable
    {
        private InputHandler inputHandler;

        public static GameState gameState;
        public static MenuState menuState;

        private GameLogic gameCoreLogic;

        private GameplayScreen gameplayScreen;
        private Dictionary<MenuState, Menu> menuScreens;

        public static ContentManager ContentManager;

        private Animation testAnimation;

        public SubGameEngine(ContentManager contentManager)
        {
            ContentManager = contentManager;

            this.gameCoreLogic = new GameLogic(contentManager);
            this.gameplayScreen = new GameplayScreen(new IDrawable[]
            {
                gameCoreLogic.TheMap
            });

            this.menuScreens = new Dictionary<MenuState, Menu>();
            this.menuScreens.Add(MenuState.MainMenu, new MainMenu());
            this.menuScreens.Add(MenuState.CharacterSelectionMenu, new CharacterSelectionMenu());

            gameState = GameState.Menu;
            menuState = MenuState.MainMenu;
            //this.menuScreens.Add(MenuState.OptionsMenu, new OptionsMenu());

            testAnimation = new Animation(ContentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png"),
                1, 582, 64, 62, 9, 0.1f);
        }

        public void Update(GameTime gameTime)
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
            testAnimation.Update(gameTime);
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
            testAnimation.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
