#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using RoseLadyLibertyOOPProject.GameObjects;
using RoseLadyLibertyOOPProject.GameObjects.Map;
using RoseLadyLibertyOOPProject.GameObjects.Units.Characters;
using RoseLadyLibertyOOPProject.GameObjects.Units.Enemies;
using RoseLadyLibertyOOPProject.GameObjects.Units;
using RoseLadyLibertyOOPProject.Core;
using RoseLadyLibertyOOPProject.Enumerations;
#endregion

namespace RoseLadyLibertyOOPProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TheGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        KeyboardState keyBoard;
        SampleCharacter character;
        EnemyManager enemyManager;
        Menu theMenu = new Menu();

        private GameState gameState;
        private InputHandler inputHandler;

        public TheGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 832;
            this.IsMouseVisible = true;
            gameState = GameState.InMenu;
            inputHandler = new InputHandler();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            map = new Map(this, 32, 32, 26, 12);
            enemyManager = new EnemyManager(map.PathTiles);
            inputHandler.AddKeyToHandle(Keys.E, enemyManager.AddEnemy);
            inputHandler.AddKeyToHandle(Keys.Escape, this.Exit);
            character = new SampleCharacter("temp", new Rectangle(0, 0, 32, 32), 10, 10, 10);
            character.Speed = 2;

            theMenu.AddMenuItem(new MenuItem(new Rectangle(500,450, 50,30)));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            inputHandler.Update();
            keyBoard = Keyboard.GetState();
            theMenu.Update(InputHandler.MouseState);
            
            HandleGameInput();
            DoPlayerBoundaries();
            HandleCollisions();
            
            enemyManager.Update();
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            map.Draw(spriteBatch);
            theMenu.Draw(spriteBatch);
            enemyManager.DrawEnemies(spriteBatch);
            character.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void HandleCollisions()
        {
            for (int i = 0; i < this.map.MapRowCells; i++)
            {
                for (int y = 0; y < this.map.MapColumnCells; y++)
                {
                    if (this.map.MapCells[i, y].TileType == Enumerations.TileType.Path 
                        && character.Rectangle.Intersects(this.map.MapCells[i, y].Rectangle))
                    {
                        if (character.MoveStatus == MoveDirection.Left)
                        {
                            character.Move(MoveDirection.Right);
                        }
                        else if (character.MoveStatus == MoveDirection.Right)
                        {
                            character.Move(MoveDirection.Left);
                        }
                        else if (character.MoveStatus == MoveDirection.Up)
                        {
                            character.Move(MoveDirection.Down);
                        }
                        else if (character.MoveStatus == MoveDirection.Down)
                        {
                            character.Move(MoveDirection.Up);
                        }
                    }
                }
            }
        }

        public void DoPlayerBoundaries()
        {
            if (character.Rectangle.X < 0)
            {
                character.Move(MoveDirection.Right);
            }
            else if (character.Rectangle.X + character.Rectangle.Width > this.map.MapWidth)
            {
                character.Move(MoveDirection.Left);
            }
            if (character.Rectangle.Y < 0)
            {
                character.Move(MoveDirection.Down);
            }
            else if (character.Rectangle.Y + character.Rectangle.Height > this.map.MapHeight)
            {
                character.Move(MoveDirection.Up);
            }
        }

        public void HandleGameInput()
        {
            if (keyBoard.IsKeyDown(Keys.D))
            {
                this.character.Move(MoveDirection.Right);
            }
            else if (keyBoard.IsKeyDown(Keys.A))
            {
                this.character.Move(MoveDirection.Left);
            }
            else if (keyBoard.IsKeyDown(Keys.W))
            {
                this.character.Move(MoveDirection.Up);
            }
            else if (keyBoard.IsKeyDown(Keys.S))
            {
                this.character.Move(MoveDirection.Down);
            }
        }
    }
}
