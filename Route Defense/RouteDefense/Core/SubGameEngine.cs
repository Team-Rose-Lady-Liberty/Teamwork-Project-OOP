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

        private Animation[] testAnimations;
        
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

            testAnimations = new Animation[]{
                new Animation(ContentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png"), 9,
                new Vector2(62, 62), new Vector2(0, 646), 5, 0, 0),
                new Animation(ContentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png"), 9,
                new Vector2(62, 62), new Vector2(0, 710), 5, 400, 0),
                new Animation(ContentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png"), 9,
                new Vector2(62, 62), new Vector2(0, 582), 5, 0, 400),
            };
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
            foreach (var animation in testAnimations)
            {
                animation.Update();
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
            foreach (var animation in testAnimations)
            {
                animation.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
