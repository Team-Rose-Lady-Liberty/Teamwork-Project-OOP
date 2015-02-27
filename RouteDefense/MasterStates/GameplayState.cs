using System.Collections.Generic;
using System.Linq;

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
using RouteDefense.UI.GameplayScreens;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.MasterStates
{
    public class GameplayState : MasterState
    {
        private readonly CharacterInfoUI characterBar;
        private CastleUI castleUI;
        private readonly List<GameObject> gameObjects;
        
        private readonly Character theCharacter;
        private readonly Map theMap;
        private readonly List<GUIElement> uiElements;
        private readonly WaveInfoUI waveBar;
        private readonly WaveManager waveManager;
        
        private int priceToUpgradeWeapon = 14;

        private Castle castle;
        
        public GameplayState(GameEngine context, Character selectedCharacter)
        {
            gameObjects = new List<GameObject>();

            Context = context;
            theCharacter = selectedCharacter;

            theMap = new Map(Context.Content, 32, 32, 32, 16);
            waveManager = new WaveManager(theMap.PathTiles, Context.Content);

            characterBar = new CharacterInfoUI();
            waveBar = new WaveInfoUI();  
            castleUI = new CastleUI();

            uiElements = new List<GUIElement>
            {
                new Button(new Rectangle(0, 515, 130, 40), Context.Textures["button"], "Next Wave",
                    delegate { waveManager.NextWave(); }),

                new Label(new Rectangle(0, 550, 100, 40), "Wave Info:"),
                new Label(new Rectangle(250, 510, 100, 40), "Castle Info:"),
                new Label(new Rectangle(500, 510, 100, 40), "Character Info:"),
                new Label(new Rectangle(800, 540, 100, 40), "Upgrades:"),

                new Button(new Rectangle(750, 580, 100, 40), Context.Textures["button"], "Castle", delegate
                {
                    if (theCharacter.Gold >= castle.PriceToUpgrade)
                    {
                        theCharacter.UpgradeArmor();
                        castle.castleHealth += 20;
                        theCharacter.Gold -= castle.PriceToUpgrade;
                        castle.PriceToUpgrade *= 2;
                    }
                }),
                new Button(new Rectangle(870, 580, 100, 40), Context.Textures["button"], "Weapon", delegate
                {
                    if (theCharacter.Gold >= priceToUpgradeWeapon)
                    {
                        theCharacter.UpgradeWeapon();
                        theCharacter.Gold -= priceToUpgradeWeapon;
                        priceToUpgradeWeapon += priceToUpgradeWeapon/2;
                    }
                })
            };

            Rectangle tempRectangle = theMap.PathTiles.Last().Rectangle;

            castle = new Castle(new Rectangle(tempRectangle.X - 2 * 32, tempRectangle.Y - 2 * 32, tempRectangle.Width * 4,
                tempRectangle.Height * 4), Context.Content.Load<Texture2D>("Castle.png"));

            gameObjects.Add(theCharacter);
            gameObjects.AddRange(theMap.PathTiles);
        }

        public override IMasterState HandleInput()
        {
            if (InputHandler.IsClicked(Keys.Escape))
            {
                return new PauseMenuState(Context);
            }
            if (InputHandler.IsHolding(Keys.A) || InputHandler.IsHolding(Keys.S)
                || InputHandler.IsHolding(Keys.W) || InputHandler.IsHolding(Keys.D))
            {
                theCharacter.IsMoving = true;
                if (InputHandler.IsHolding(Keys.D))
                {
                    theCharacter.Move(MoveDirection.Right);
                }
                else if (InputHandler.IsHolding(Keys.A))
                {
                    theCharacter.Move(MoveDirection.Left);
                }
                else if (InputHandler.IsHolding(Keys.W))
                {
                    theCharacter.Move(MoveDirection.Up);
                }
                else if (InputHandler.IsHolding(Keys.S))
                {
                    theCharacter.Move(MoveDirection.Down);
                }
            }
            else if (!InputHandler.IsHolding(Keys.A) && !InputHandler.IsHolding(Keys.S)
                     && !InputHandler.IsHolding(Keys.W) && !InputHandler.IsHolding(Keys.D))
            {
                theCharacter.IsMoving = false;
            }

            if (InputHandler.IsHolding(Keys.Space))
            {
                if (theCharacter.CanAttack)
                    theCharacter.IsAttacking = true;
            }

            return null;
        }

        public override IMasterState Update(GameTime gameTime)
        {
            characterBar.Update(theCharacter);
            waveBar.Update(waveManager.CurrentWave);
            castleUI.Upgrade(castle);

            foreach (var element in uiElements)
            {
                element.Update(gameTime);
            }

            theCharacter.Update(gameTime, theMap, waveManager.GetSpawnedList());

            HandleCollisions();

            DoPlayerBoundaries();

            waveManager.Update(gameTime);
            foreach (var enemy in waveManager.GetSpawnedList())
            {
                if (enemy.Rectangle.Intersects(castle.Rectangle))
                {
                    castle.castleHealth -= enemy.Attack;
                    enemy.Kill();
                }
            }

            if (castle.castleHealth <= 0)
            {
                Context.DeleteState(this);
                PathGenerator.Path.curve = 1;
                return new MainMenuState(Context);
            }

            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            characterBar.Draw(spriteBatch);
            waveBar.Draw(spriteBatch);
            castleUI.Draw(spriteBatch);

            spriteBatch.Begin();
            foreach (var element in uiElements)
            {
                element.Draw(spriteBatch);
            }       
            theMap.Draw(spriteBatch);
            castle.Draw(spriteBatch);
            waveManager.Draw(spriteBatch);
            theCharacter.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void HandleCollisions()
        {
            for (var first = 0; first < gameObjects.Count; first++)
            {
                for (var second = 0; second < gameObjects.Count; second++)
                {
                    if (first != second)
                    {
                        if (gameObjects[first] is Character && gameObjects[second] is Tile
                            &&
                            ((Character) gameObjects[first]).ActualRectangle.Intersects(gameObjects[second].Rectangle))
                        {
                            ((Character) gameObjects[first]).ReturnLastPosition();
                        }
                    }
                }
            }
        }

        public void DoPlayerBoundaries()
        {
            if (theCharacter.ActualRectangle.X < 0
                || theCharacter.ActualRectangle.X + theCharacter.ActualRectangle.Width > theMap.MapWidth
                || theCharacter.ActualRectangle.Y < 0
                || theCharacter.ActualRectangle.Y + theCharacter.ActualRectangle.Height > theMap.MapHeight)
            {
                theCharacter.ReturnLastPosition();
            }
        }
    }
}