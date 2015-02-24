using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;
using RouteDefense.Core.Gameplay;
using RouteDefense.Enumerations;
using RouteDefense.Models;
using RouteDefense.Models.GameObjects;
using RouteDefense.Models.GameObjects.Units;
using RouteDefense.UI;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.MasterStates
{
    public class GameplayState : MasterState
    {
        private Map theMap;
        private Character theCharacter;
        private WaveManager waveManager;

        private const int timeBetweenWave = 10;

        private List<GUIElement> uiElements;

        private Stopwatch timer;

        private List<GameObject> gameObjects; 

        public GameplayState(GameEngine context, Character selectedCharacter)
        {
            gameObjects = new List<GameObject>();

            Context = context;
            theCharacter = selectedCharacter;

            timer = new Stopwatch();
            timer.Start();

            this.theMap = new Map(Context.Content, 32, 32, 32, 16);
            
            waveManager = new WaveManager(this.theMap.PathTiles, Context.Content);

            uiElements = new List<GUIElement>()
            {
                new Button(new Rectangle(0, 515, 130, 40), Context.Textures["button"], "Next Wave",
                    delegate() { waveManager.Start();  }),

                new Label(new Rectangle(0, 550, 100,40), "Wave Info:"),
                new Label(new Rectangle(0, 575, 100,40), "Enemy Health:"),
                new Label(new Rectangle(0, 600, 100,40), "Gold per unit:"),
                new Label(new Rectangle(0, 625, 100,40), "Enemy Damage:"),
                new Label(new Rectangle(0, 650, 100,40), "Enemy Speed:"),


                new Label(new Rectangle(250, 510, 100,40), "Castle Info:"),
                new Label(new Rectangle(250, 535, 100,40), "Health:"),
                new Label(new Rectangle(250, 570, 100,40), "Level:"),
                new Label(new Rectangle(250, 595, 100,40), "Gold to upgrade:"),


                new Label(new Rectangle(500, 510, 100,40), "Character Info:"),
                new Label(new Rectangle(500, 535, 100,40), "Class:"),
                new Label(new Rectangle(500, 570, 100,40), "Attack damage:"),
                new Label(new Rectangle(500, 595, 100,40), "Attack speed:"),
                new Label(new Rectangle(500, 620, 100,40), "Movement speed:"),

                new Label(new Rectangle(750, 510, 100,40), "Gold:"),
                new Label(new Rectangle(800, 540, 100,40), "Upgrades:"),
                new Button(new Rectangle(750, 580, 100,40), Context.Textures["button"], "Castle", delegate(){}),
                new Button(new Rectangle(870, 580, 100,40), Context.Textures["button"], "Weapon", delegate(){}),
                
            };

            gameObjects.Add(theCharacter);
            gameObjects.AddRange(theMap.PathTiles);
        }


        public override IMasterState HandleInput()
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {
                return new PauseMenuState(Context);
            }
            if (InputHandler.KeyboardState.IsKeyDown(Keys.A) || InputHandler.KeyboardState.IsKeyDown(Keys.S)
                || InputHandler.KeyboardState.IsKeyDown(Keys.W) || InputHandler.KeyboardState.IsKeyDown(Keys.D))
            {
                theCharacter.IsMoving = true;
                if (InputHandler.KeyboardState.IsKeyDown(Keys.D))
                {
                    this.theCharacter.Move(MoveDirection.Right);
                    this.theCharacter.AnimState = MoveDirection.Right;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.A))
                {
                    this.theCharacter.Move(MoveDirection.Left);
                    this.theCharacter.AnimState = MoveDirection.Left;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.W))
                {
                    this.theCharacter.Move(MoveDirection.Up);
                    this.theCharacter.AnimState = MoveDirection.Up;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
                {
                    this.theCharacter.Move(MoveDirection.Down);
                    this.theCharacter.AnimState = MoveDirection.Down;
                }
            }
            else if (InputHandler.KeyboardState.IsKeyUp(Keys.A) && InputHandler.KeyboardState.IsKeyUp(Keys.S)
                     && InputHandler.KeyboardState.IsKeyUp(Keys.W) && InputHandler.KeyboardState.IsKeyUp(Keys.D))
            {
                theCharacter.IsMoving = false;
            }

            if (InputHandler.KeyboardState.IsKeyDown(Keys.Space))
            {
                theCharacter.IsAttacking = true;
            }

            return null;
        }

        public override IMasterState Update(GameTime gameTime)
        {
            foreach (var element in uiElements)
            {
                element.Update(gameTime);
            }

            theCharacter.Update(gameTime);
            HandleCollisions();
            DoPlayerBoundaries();
            waveManager.Update(gameTime);
            InteractionWithEnemies();
            
            return null;
        }
 
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var element in uiElements)
            {
                element.Draw(spriteBatch);
            }
            theMap.Draw(spriteBatch);
            waveManager.Draw(spriteBatch);
            theCharacter.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void InteractionWithEnemies()
        {
            var enemies = theCharacter.GetTargets(waveManager.GetSpawnedList());
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Rectangle.Contains(GameEngine.CurrentMouseState.Position))
                {
                    Context.MouseCursor = Context.AttackCursor;
                    if (GameEngine.CurrentMouseState.LeftButton == ButtonState.Pressed &&
                        GameEngine.OldMouseState.LeftButton == ButtonState.Released)
                    {
                        enemies[i].Health -= 50;
                        if (enemies[i].Health <= 0)
                            enemies[i].Kill();
                    }
                }
                else
                {
                    Context.MouseCursor = Context.NormalCursor;
                }
            }
        }

        public void HandleCollisions()
        {
            for (int first = 0; first < gameObjects.Count; first++)
            {
                for (int second = 0; second < gameObjects.Count; second++)
                {
                    if (first != second)
                    {
                        if (gameObjects[first] is Character && gameObjects[second] is Tile
                            &&
                            ((Character) gameObjects[first]).ActualRectangle.Intersects(gameObjects[second].Rectangle))
                        {
                            ((Character)gameObjects[first]).ReturnLastPosition();
                        }
                    }
                }
            }        
        }

        public void DoPlayerBoundaries()
        {
            if (theCharacter.ActualRectangle.X < 0 
                || theCharacter.ActualRectangle.X + theCharacter.ActualRectangle.Width > this.theMap.MapWidth
                || theCharacter.ActualRectangle.Y < 0
                || theCharacter.ActualRectangle.Y + theCharacter.ActualRectangle.Height > this.theMap.MapHeight)
            {
                theCharacter.ReturnLastPosition();
            }  
        }
    }
}
