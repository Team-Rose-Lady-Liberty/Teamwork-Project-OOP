using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RouteDefense.Enumerations;
using RouteDefense.Models;
using RouteDefense.Models.GameScreens;

using IDrawable = RouteDefense.Interfaces.IDrawable;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.Core
{
    public class SubGameEngine : IDrawable, IUpdateable
    {
        private InputHandler inputHandler;

        public static GameState CurrentGameState;
        public static ContentManager ContentManager;

        private Dictionary<GameState, GameScreen> gameScreens;

        public SubGameEngine(ContentManager contentManager)
        {
            ContentManager = contentManager;

            inputHandler = new InputHandler();
           
            this.gameScreens = new Dictionary<GameState, GameScreen>();
            this.gameScreens.Add(GameState.MainMenu, new MainMenu());
            this.gameScreens.Add(GameState.CharacterSelection, new CharacterSelectionMenu());
            this.gameScreens.Add(GameState.Game, new GameplayScreen());

            CurrentGameState = GameState.MainMenu;
        }

        public void Update(GameTime gameTime)
        {
            inputHandler.Update();
            this.gameScreens[CurrentGameState].HandleInput(inputHandler);

            this.gameScreens[CurrentGameState].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            this.gameScreens[CurrentGameState].Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
