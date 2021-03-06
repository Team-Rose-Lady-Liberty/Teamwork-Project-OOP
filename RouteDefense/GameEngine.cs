﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using RouteDefense.Core;
using RouteDefense.MasterStates;

namespace RouteDefense  
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<IMasterState> masterStates;

        public IMasterState CurrentState;

        public Texture2D MouseCursor { get; set; }

        private InputHandler inputHandler;

        public const int WindowWidth = 1024;
        public const int WindowHeight = 672;

        public Dictionary<string, Texture2D> Textures;

        public static SpriteFont GameFont;

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            //this.IsMouseVisible = true;
            inputHandler = new InputHandler();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            MouseCursor = Content.Load<Texture2D>("Mouse\\normal.png");

            Textures = new Dictionary<string, Texture2D>();
            Textures.Add("button", Content.Load<Texture2D>("Menu Items\\blue_button03.png"));
            GameFont = Content.Load<SpriteFont>("Fonts\\TestFont");

            masterStates = new List<IMasterState>();
            CurrentState = new MainMenuState(this);

            masterStates.Add(CurrentState);

            base.Initialize();
        }

        public void DeleteState(IMasterState state)
        {
            masterStates.Remove(state);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            this.HandleInput();

            IMasterState returnedState = CurrentState.Update(gameTime);
            ChangeState(returnedState);

            base.Update(gameTime);
        }

        public void HandleInput()
        {
            inputHandler.Update();

            IMasterState returnedState = CurrentState.HandleInput();
            ChangeState(returnedState);
        }

        public List<IMasterState> GetStates()
        {
            return this.masterStates;
        }

        public void ChangeState(IMasterState paramState)
        {
            if (paramState != null)
            {
                IMasterState queryState = masterStates.Find(state => state.GetType() == paramState.GetType());
                if (queryState != null)
                {
                    CurrentState = queryState;
                }
                else
                {
                    CurrentState = paramState;
                    masterStates.Add(CurrentState);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            CurrentState.Draw(spriteBatch);
            spriteBatch.Begin();

            spriteBatch.Draw(MouseCursor, new Vector2(InputHandler.CurrentMouseState.X, InputHandler.CurrentMouseState.Y), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
