using System.Collections.Generic;
using System.Diagnostics;
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
        private Map theMap;
        private Character theCharacter;
        private WaveManager waveManager;

        private List<GUIElement> uiElements;

        private Stopwatch timer;

        private List<GameObject> gameObjects;

        private CharacterInfoUI characterBar;
        private WaveInfoUI waveBar;

        private int castleHealth = 100;
        private GameObject castleObject;

        private int priceToUpgradeCastle = 20;

        private int priceToUpgradeWeapon = 14;

        private Texture2D castleTexture;
        private Rectangle castleRectangle;

        private int castleLevel = 1;
        private Label castleHealthLabel;
        private Label priceToUpgradeCastleLabel;
        private Label castleLevelLabel;

        public GameplayState(GameEngine context, Character selectedCharacter)
        {
            gameObjects = new List<GameObject>();

            Context = context;
            theCharacter = selectedCharacter;

            timer = new Stopwatch();
            timer.Start();

            this.theMap = new Map(Context.Content, 32, 32, 32, 16);
            
            waveManager = new WaveManager(this.theMap.PathTiles, Context.Content);

            characterBar = new CharacterInfoUI(Vector2.Zero);
            waveBar = new WaveInfoUI(Vector2.Zero);

            castleHealthLabel = new Label(new Rectangle(250, 535, 100, 40), "Health:");
            priceToUpgradeCastleLabel = new Label(new Rectangle(250, 595, 100, 40), "Gold to upgrade:");
            castleLevelLabel = new Label(new Rectangle(250, 570, 100, 40), "Level:");

            uiElements = new List<GUIElement>()
            {
                new Button(new Rectangle(0, 515, 130, 40), Context.Textures["button"], "Next Wave",
                    delegate() { waveManager.NextWave();  }),

                new Label(new Rectangle(0, 550, 100,40), "Wave Info:"),

                new Label(new Rectangle(250, 510, 100,40), "Castle Info:"),
                
                new Label(new Rectangle(500, 510, 100,40), "Character Info:"),

                new Label(new Rectangle(800, 540, 100,40), "Upgrades:"),
                new Button(new Rectangle(750, 580, 100,40), Context.Textures["button"], "Castle", delegate()
                {
                    if (theCharacter.Gold >= priceToUpgradeCastle)
                    {
                        theCharacter.UpgradeArmor();
                        castleHealth += 20;
                        theCharacter.Gold -= priceToUpgradeCastle;
                        priceToUpgradeCastle *= 2;             
                    }
                }),
                new Button(new Rectangle(870, 580, 100,40), Context.Textures["button"], "Weapon", delegate()
                {
                    if (theCharacter.Gold >= priceToUpgradeWeapon)
                    {
                        theCharacter.UpgradeWeapon();
                        theCharacter.Gold -= priceToUpgradeWeapon;
                        priceToUpgradeWeapon += priceToUpgradeWeapon / 2;    
                    }
                }),
                
            };

            castleTexture = Context.Content.Load<Texture2D>("Castle.png");

            castleRectangle = theMap.PathTiles.Last().Rectangle;
            castleRectangle = new Rectangle(castleRectangle.X - 2 * 32, castleRectangle.Y - 2 * 32, castleTexture.Width / 3, castleTexture.Height / 3);

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
                    this.theCharacter.Move(MoveDirection.Right);
                }
                else if (InputHandler.IsHolding(Keys.A))
                {
                    this.theCharacter.Move(MoveDirection.Left);
                }
                else if (InputHandler.IsHolding(Keys.W))
                {
                    this.theCharacter.Move(MoveDirection.Up);
                }
                else if (InputHandler.IsHolding(Keys.S))
                {
                    this.theCharacter.Move(MoveDirection.Down);
                }
            }
            else if (!InputHandler.IsHolding(Keys.A) && !InputHandler.IsHolding(Keys.S)
                     && !InputHandler.IsHolding(Keys.W) && !InputHandler.IsHolding(Keys.D))
            {
                theCharacter.IsMoving = false;
            }

            if (InputHandler.IsHolding(Keys.Space))
            {
                if(theCharacter.CanAttack)
                    theCharacter.IsAttacking = true;
            }

            return null;
        }

        public override IMasterState Update(GameTime gameTime)
        {
            characterBar.Update(theCharacter);
            waveBar.Update(waveManager.CurrentWave);

            castleHealthLabel.text = "Health: " + castleHealth;
            priceToUpgradeCastleLabel.text = "Gold to upgrade: " + priceToUpgradeCastle;
            castleLevelLabel.text = "Level: " + castleLevel;

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
                if (enemy.Rectangle.Intersects(castleRectangle))
                {
                    castleHealth -= enemy.Attack;
                    enemy.Kill();
                }
            }
            return null;
        }
 
        public override void Draw(SpriteBatch spriteBatch)
        {
            characterBar.Draw(spriteBatch);
            waveBar.Draw(spriteBatch);
            
            spriteBatch.Begin();

            priceToUpgradeCastleLabel.Draw(spriteBatch);
            castleHealthLabel.Draw(spriteBatch);
            castleLevelLabel.Draw(spriteBatch);
            foreach (var element in uiElements)
            {
                element.Draw(spriteBatch);
            }

            theMap.Draw(spriteBatch);
            spriteBatch.Draw(castleTexture, castleRectangle, Color.White);
            waveManager.Draw(spriteBatch);
            theCharacter.Draw(spriteBatch);
            spriteBatch.End();
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
                            && ((Character) gameObjects[first]).ActualRectangle.Intersects(gameObjects[second].Rectangle))
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
