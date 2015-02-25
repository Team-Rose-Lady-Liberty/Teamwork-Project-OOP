using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using RouteDefense.MasterStates;

namespace RouteDefense  
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState OldKeyboardState;

        public static MouseState CurrentMouseState;
        public static MouseState OldMouseState;


        private List<IMasterState> masterStates;

        public IMasterState CurrentState;

        public Texture2D MouseCursor { get; set; }

        public Texture2D NormalCursor { get; private set; }
        public Texture2D AttackCursor { get; private set; }

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

            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            NormalCursor = Content.Load<Texture2D>("Mouse\\normal.png");
            AttackCursor = Content.Load<Texture2D>("Mouse\\attack.png");

            MouseCursor = NormalCursor;

            Textures = new Dictionary<string, Texture2D>();
            Textures.Add("button", Content.Load<Texture2D>("Menu Items\\button.png"));
            GameFont = Content.Load<SpriteFont>("Fonts\\TestFont");

            masterStates = new List<IMasterState>();
            CurrentState = new MainMenuState(this);

            masterStates.Add(CurrentState);

            base.Initialize();
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
            OldMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            OldKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

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

            spriteBatch.Draw(MouseCursor, new Vector2(CurrentMouseState.X, CurrentMouseState.Y), Color.White);

            //spriteBatch.Draw(Textures["button"], new Rectangle(CurrentMouseState.X, CurrentMouseState.Y, 3,3), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
