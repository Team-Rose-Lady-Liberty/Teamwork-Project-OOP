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
        Tile[] enemyPath;
        List<Enemy> enemies;


        bool ePressed;
        public TheGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 832;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map(this, 32, 32, 26, 12);
            enemyPath = map.PathTiles;
            character = new SampleCharacter("temp", new Rectangle(0, 0, 32, 32), 10, 10, 10);
            enemies = new List<Enemy>();
            character.Speed = 2;
            ePressed = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyBoard = Keyboard.GetState();
            
            if(keyBoard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(keyBoard.IsKeyDown(Keys.E))
            {
                if (ePressed == false)
                {
                    enemies.Add(new SampleEnemy("sd", enemyPath[0].Rectangle, 10, 10, 10));
                    ePressed = true;
                }
            }
            if(keyBoard.IsKeyUp(Keys.E))
            {
                ePressed = false;
            }
            Enumerations.MoveDirection direction = Enumerations.MoveDirection.None;
            if(keyBoard.IsKeyDown(Keys.D))
            {
                direction = Enumerations.MoveDirection.Right;
            }
            else if (keyBoard.IsKeyDown(Keys.A))
            {
                direction = Enumerations.MoveDirection.Left;
            }
            else if (keyBoard.IsKeyDown(Keys.W))
            {
                direction = Enumerations.MoveDirection.Up;
            }
            else if (keyBoard.IsKeyDown(Keys.S))
            {
                direction = Enumerations.MoveDirection.Down;
            }
            this.character.Move(direction);
            if(character.Rectangle.X < 0)
            {
                character.Move(Enumerations.MoveDirection.Right);
            }
            else if (character.Rectangle.X + character.Rectangle.Width > this.map.MapWidth)
            {
                character.Move(Enumerations.MoveDirection.Left);
            }
            if (character.Rectangle.Y < 0)
            {
                character.Move(Enumerations.MoveDirection.Down);
            }
            else if (character.Rectangle.Y + character.Rectangle.Height > this.map.MapHeight)
            {
                character.Move(Enumerations.MoveDirection.Up);
            }

            for (int i = 0; i < this.map.MapRowCells; i++)
            {
                for (int y = 0; y < this.map.MapColumnCells; y++)
                {
                    if(this.map.MapCells[i,y].TileType == Enumerations.TileType.Path && character.Rectangle.Intersects(this.map.MapCells[i, y].Rectangle))
                    {
                        if (direction == Enumerations.MoveDirection.Left)
                            character.Move(Enumerations.MoveDirection.Right);
                        else if (direction == Enumerations.MoveDirection.Right)
                            character.Move(Enumerations.MoveDirection.Left);
                        else if (direction == Enumerations.MoveDirection.Up)
                            character.Move(Enumerations.MoveDirection.Down);
                        else if (direction == Enumerations.MoveDirection.Down)
                            character.Move(Enumerations.MoveDirection.Up);
                    }
                }
            }
            for(int i = 0; i < enemies.Count;i++)
            {
                enemies[i].Update(enemyPath);
                if (enemies[i].AtFinish)
                    enemies.Remove(enemies[i]);
            }
                // TODO: Add your update logic here
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            map.Draw(spriteBatch);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            character.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
