using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RouteDefense.Core.Gameplay;
using RouteDefense.Enumerations;
using RouteDefense.Models.GameObjects.Units;

namespace RouteDefense.Core
{
    public class GameLogic
    {
        public Map TheMap { get; private set; }
        public Character TheCharacter { get; private set; }
        private WaveManager waveManager;
        private InputHandler inputHandler;

        public GameLogic(ContentManager contentManager)
        {
            this.TheMap = new Map(contentManager, 32, 32, 26, 12);
            //TheCharacter = new SampleCharacter("temp", new Rectangle(0, 0, 32, 32), 10, 10, 10);
            //TheCharacter.Speed = 2;

            waveManager = new WaveManager(this.TheMap.PathTiles);

            this.inputHandler = new InputHandler();
            //this.inputHandler.AddKeyToHandle(Keys.E, enemyManager.AddEnemy);
        }

        public void Update()
        {
            this.inputHandler.Update();
            TheMap.Update();
            waveManager.Update();

            //HandleGameInput();
            //DoPlayerBoundaries();
            //HandleCollisions();
        }

        public void HandleCollisions()
        {
            for (int i = 0; i < this.TheMap.MapRowCells; i++)
            {
                for (int y = 0; y < this.TheMap.MapColumnCells; y++)
                {
                    if (this.TheMap.MapCells[i, y].TileType == Enumerations.TileType.Path
                        && TheCharacter.Rectangle.Intersects(this.TheMap.MapCells[i, y].Rectangle))
                    {
                        if (TheCharacter.MoveState == MoveDirection.Left)
                        {
                            TheCharacter.Move(MoveDirection.Right);
                        }
                        else if (TheCharacter.MoveState == MoveDirection.Right)
                        {
                            TheCharacter.Move(MoveDirection.Left);
                        }
                        else if (TheCharacter.MoveState == MoveDirection.Up)
                        {
                            TheCharacter.Move(MoveDirection.Down);
                        }
                        else if (TheCharacter.MoveState == MoveDirection.Down)
                        {
                            TheCharacter.Move(MoveDirection.Up);
                        }
                    }
                }
            }
        }

        public void DoPlayerBoundaries()
        {
            if (TheCharacter.Rectangle.X < 0)
            {
                TheCharacter.Move(MoveDirection.Right);
            }
            else if (TheCharacter.Rectangle.X + TheCharacter.Rectangle.Width > this.TheMap.MapWidth)
            {
                TheCharacter.Move(MoveDirection.Left);
            }
            if (TheCharacter.Rectangle.Y < 0)
            {
                TheCharacter.Move(MoveDirection.Down);
            }
            else if (TheCharacter.Rectangle.Y + TheCharacter.Rectangle.Height > this.TheMap.MapHeight)
            {
                TheCharacter.Move(MoveDirection.Up);
            }
        }

        public void HandleGameInput()
        {  
            if (InputHandler.KeyboardState.IsKeyDown(Keys.D))
            {
                this.TheCharacter.Move(MoveDirection.Right);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.A))
            {
                this.TheCharacter.Move(MoveDirection.Left);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.W))
            {
                this.TheCharacter.Move(MoveDirection.Up);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
            {
                this.TheCharacter.Move(MoveDirection.Down);
            }
        }
    }
}
