﻿using System.Collections.Generic;
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

        public Wave tempWave;

        public GameLogic(ContentManager contentManager)
        {
            this.TheMap = new Map(contentManager, 32, 32, 26, 12);
            TheCharacter = new Character("test", new Rectangle(0, 0, 48, 48));
            TheCharacter.Speed = 2;

            waveManager = new WaveManager(this.TheMap.PathTiles);
            tempWave = new Wave(10);
            this.inputHandler = new InputHandler();
        }

        public void Update(GameTime gameTime)
        {
            this.inputHandler.Update();
            TheMap.Update();
            waveManager.Update(gameTime);
            tempWave.Update(gameTime, this.TheMap.PathTiles);
            TheCharacter.Update(gameTime);
            InteractionWithEnemis();
            HandleGameInput();
            DoPlayerBoundaries();
            HandleCollisions();
        }

        private bool clicked = false;
        private void InteractionWithEnemis()
        {
            if (InputHandler.MouseState.LeftButton == ButtonState.Pressed && clicked == false)
            {
                clicked = true;
                var enemies = TheCharacter.GetTargets(tempWave.GetEnemies());
                
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
            if (InputHandler.KeyboardState.IsKeyDown(Keys.A) || InputHandler.KeyboardState.IsKeyDown(Keys.S)
                || InputHandler.KeyboardState.IsKeyDown(Keys.W) || InputHandler.KeyboardState.IsKeyDown(Keys.D))
            {
                TheCharacter.IsMoving = true;
                if (InputHandler.KeyboardState.IsKeyDown(Keys.D))
                {
                    this.TheCharacter.Move(MoveDirection.Right);
                    this.TheCharacter.AnimState = MoveDirection.Right;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.A))
                {
                    this.TheCharacter.Move(MoveDirection.Left);
                    this.TheCharacter.AnimState = MoveDirection.Left;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.W))
                {
                    this.TheCharacter.Move(MoveDirection.Up);
                    this.TheCharacter.AnimState = MoveDirection.Up;
                }
                else if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
                {
                    this.TheCharacter.Move(MoveDirection.Down);
                    this.TheCharacter.AnimState = MoveDirection.Down;
                }
            }
            else if (InputHandler.KeyboardState.IsKeyUp(Keys.A) && InputHandler.KeyboardState.IsKeyUp(Keys.S)
                     && InputHandler.KeyboardState.IsKeyUp(Keys.W) && InputHandler.KeyboardState.IsKeyUp(Keys.D))
            {
                TheCharacter.IsMoving = false;
            }
        }
    }
}
