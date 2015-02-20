using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core;
using RouteDefense.Core.Gameplay;
using RouteDefense.Enumerations;
using RouteDefense.Models.GameObjects.Units;

namespace RouteDefense.Models.GameScreens
{
    public class GameplayScreen : GameScreen
    {
        private Map theMap;
        private Character theCharacter;
        private WaveManager waveManager;

        public GameplayScreen()
        {
            this.theMap = new Map(SubGameEngine.ContentManager, 32, 32, 26, 12);
            theCharacter = new Character("test", new Rectangle(0, 0, 48, 48));
            theCharacter.Speed = 2;

            waveManager = new WaveManager(this.theMap.PathTiles);
        }


        public override void HandleInput(InputHandler inputHandler)
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.Escape))
            {

                SubGameEngine.CurrentGameState = GameState.MainMenu;
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

            if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
            {
                waveManager.Start();
            }
        }

        public override void Update(GameTime gameTime)
        {
            waveManager.Update(gameTime);

            theCharacter.Update(gameTime);
            InteractionWithEnemis();
            DoPlayerBoundaries();
            HandleCollisions();
        }
 
        public override void Draw(SpriteBatch spriteBatch)
        {
            theMap.Draw(spriteBatch);
            waveManager.Draw(spriteBatch);
            theCharacter.Draw(spriteBatch);
        }

        private bool clicked = false;

        private void InteractionWithEnemis()
        {
            if (InputHandler.MouseState.LeftButton == ButtonState.Pressed && clicked == false)
            {
                clicked = true;
                var enemies = theCharacter.GetTargets(waveManager.GetSpawnedList());

                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].Rectangle.Contains(InputHandler.MouseState.Position))
                    {
                        enemies[i].Health -= 50;
                        if (enemies[i].Health <= 0)
                            enemies[i].AtFinish = true;
                    }
                }
            }

            if (InputHandler.MouseState.LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
        }

        public void HandleCollisions()
        {
            for (int i = 0; i < this.theMap.MapRowCells; i++)
            {
                for (int y = 0; y < this.theMap.MapColumnCells; y++)
                {
                    if (this.theMap.MapCells[i, y].TileType == Enumerations.TileType.Path
                        && theCharacter.ActualRectangle.Intersects(this.theMap.MapCells[i, y].Rectangle))
                    {
                        if (theCharacter.MoveState == MoveDirection.Left)
                        {
                            theCharacter.Move(MoveDirection.Right);
                        }
                        else if (theCharacter.MoveState == MoveDirection.Right)
                        {
                            theCharacter.Move(MoveDirection.Left);
                        }
                        else if (theCharacter.MoveState == MoveDirection.Up)
                        {
                            theCharacter.Move(MoveDirection.Down);
                        }
                        else if (theCharacter.MoveState == MoveDirection.Down)
                        {
                            theCharacter.Move(MoveDirection.Up);
                        }
                    }
                }
            }
        }

        public void DoPlayerBoundaries()
        {
            if (theCharacter.Rectangle.X < 0)
            {
                theCharacter.Move(MoveDirection.Right);
            }
            else if (theCharacter.Rectangle.X + theCharacter.Rectangle.Width > this.theMap.MapWidth)
            {
                theCharacter.Move(MoveDirection.Left);
            }
            if (theCharacter.Rectangle.Y < 0)
            {
                theCharacter.Move(MoveDirection.Down);
            }
            else if (theCharacter.Rectangle.Y + theCharacter.Rectangle.Height > this.theMap.MapHeight)
            {
                theCharacter.Move(MoveDirection.Up);
            }
        }
    }
}
