using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using RoseLadyLibertyOOPProject.Enumerations;
using RoseLadyLibertyOOPProject.GameObjects;
using RoseLadyLibertyOOPProject.GameObjects.Map;
using RoseLadyLibertyOOPProject.GameObjects.Units.Characters;

namespace RoseLadyLibertyOOPProject.Core
{
    public class GameLogic
    {
        public Map theMap;
        public Character theCharacter;
        private EnemyManager enemyManager;
        private InputHandler inputHandler;

        public GameLogic(ContentManager contentManager)
        {
            this.theMap = new Map(contentManager, 32, 32, 26, 12);
            theCharacter = new SampleCharacter("temp", new Rectangle(0, 0, 32, 32), 10, 10, 10);
            theCharacter.Speed = 2;

            enemyManager = new EnemyManager(this.theMap.PathTiles);

            this.inputHandler = new InputHandler();
            this.inputHandler.AddKeyToHandle(Keys.E, enemyManager.AddEnemy);
        }

        public void Update()
        {
            this.inputHandler.Update();
            theMap.Update();
            enemyManager.Update();
            HandleGameInput();
            DoPlayerBoundaries();
            HandleCollisions();
        }

        public void HandleCollisions()
        {
            for (int i = 0; i < this.theMap.MapRowCells; i++)
            {
                for (int y = 0; y < this.theMap.MapColumnCells; y++)
                {
                    if (this.theMap.MapCells[i, y].TileType == Enumerations.TileType.Path
                        && theCharacter.Rectangle.Intersects(this.theMap.MapCells[i, y].Rectangle))
                    {
                        if (theCharacter.MoveStatus == MoveDirection.Left)
                        {
                            theCharacter.Move(MoveDirection.Right);
                        }
                        else if (theCharacter.MoveStatus == MoveDirection.Right)
                        {
                            theCharacter.Move(MoveDirection.Left);
                        }
                        else if (theCharacter.MoveStatus == MoveDirection.Up)
                        {
                            theCharacter.Move(MoveDirection.Down);
                        }
                        else if (theCharacter.MoveStatus == MoveDirection.Down)
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

        public void HandleGameInput()
        {  
            if (InputHandler.KeyboardState.IsKeyDown(Keys.D))
            {
                this.theCharacter.Move(MoveDirection.Right);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.A))
            {
                this.theCharacter.Move(MoveDirection.Left);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.W))
            {
                this.theCharacter.Move(MoveDirection.Up);
            }
            else if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
            {
                this.theCharacter.Move(MoveDirection.Down);
            }
        }
    }
}
