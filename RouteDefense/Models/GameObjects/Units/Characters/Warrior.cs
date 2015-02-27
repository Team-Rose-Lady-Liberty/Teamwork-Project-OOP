using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core.Gameplay;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    class Warrior : Character
    {
        private Dictionary<string, Dictionary<MoveDirection, Animation>> specificAnimations;

        public string WeaponType;

        public Rectangle temp;

        public Warrior(Rectangle rectangle, ContentManager contentManager)
            : base(rectangle, contentManager, Constants.WarriorDefaultRange, Constants.WarriorDefaultMovementSpeed,
            Constants.WarriorDefaultAttackDamage, Constants.WarriorDefaultAttackSpeed)
        {
            WeaponType = "";
            textures = new Dictionary<string, Texture2D>();
            specificAnimations = new Dictionary<string, Dictionary<MoveDirection, Animation>>();

            maxWeaponLevel = 4;
            maxArmorLevel = 3;

            LoadContent(contentManager);

            currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];

            attackAnimations.Add(MoveDirection.Down, new Animation(0, 384, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Up, new Animation(0, 256, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Left, new Animation(0, 320, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Right, new Animation(0, 448, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
        
            Dictionary<MoveDirection, Animation> sword = new Dictionary<MoveDirection, Animation>();
            sword.Add(MoveDirection.Up, new Animation(0, 1344, 192, 192, 6, 0.1f));
            sword.Add(MoveDirection.Left, new Animation(0, 1536, 192, 192, 6, 0.1f));
            sword.Add(MoveDirection.Right, new Animation(0, 1920, 192, 192, 6, 0.1f));
            sword.Add(MoveDirection.Down, new Animation(0, 1728, 192, 192, 6, 0.1f));

            Dictionary<MoveDirection, Animation> spear = new Dictionary<MoveDirection, Animation>();
            spear.Add(MoveDirection.Up, new Animation(0, 1344, 192, 192, 8, 0.1f));
            spear.Add(MoveDirection.Left, new Animation(0, 1536, 192, 192, 8, 0.1f));
            spear.Add(MoveDirection.Right, new Animation(0, 1920, 192, 192, 8, 0.1f));
            spear.Add(MoveDirection.Down, new Animation(0, 1728, 192, 192, 8, 0.1f));

            specificAnimations.Add("sword", sword);
            specificAnimations.Add("spear", spear);

            temp = new Rectangle(0, 0,0,0);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            textures.Add("Armor0Weapon0", contentManager.Load<Texture2D>("WarriorSprites\\Armor0Weapon0.png"));
            textures.Add("Armor0Weapon1", contentManager.Load<Texture2D>("WarriorSprites\\Armor0Weapon1.png"));
            textures.Add("Armor0Weapon2", contentManager.Load<Texture2D>("WarriorSprites\\Armor0Weapon2.png"));
            textures.Add("Armor0Weapon3", contentManager.Load<Texture2D>("WarriorSprites\\Armor0Weapon3.png"));
            textures.Add("Armor0Weapon4", contentManager.Load<Texture2D>("WarriorSprites\\Armor0Weapon4.png"));

            textures.Add("Armor1Weapon0", contentManager.Load<Texture2D>("WarriorSprites\\Armor1Weapon0.png"));
            textures.Add("Armor1Weapon1", contentManager.Load<Texture2D>("WarriorSprites\\Armor1Weapon1.png"));
            textures.Add("Armor1Weapon2", contentManager.Load<Texture2D>("WarriorSprites\\Armor1Weapon2.png"));
            textures.Add("Armor1Weapon3", contentManager.Load<Texture2D>("WarriorSprites\\Armor1Weapon3.png"));
            textures.Add("Armor1Weapon4", contentManager.Load<Texture2D>("WarriorSprites\\Armor1Weapon4.png"));


            textures.Add("Armor2Weapon0", contentManager.Load<Texture2D>("WarriorSprites\\Armor2Weapon0.png"));
            textures.Add("Armor2Weapon1", contentManager.Load<Texture2D>("WarriorSprites\\Armor2Weapon1.png"));
            textures.Add("Armor2Weapon2", contentManager.Load<Texture2D>("WarriorSprites\\Armor2Weapon2.png"));
            textures.Add("Armor2Weapon3", contentManager.Load<Texture2D>("WarriorSprites\\Armor2Weapon3.png"));
            textures.Add("Armor2Weapon4", contentManager.Load<Texture2D>("WarriorSprites\\Armor2Weapon4.png"));

            textures.Add("Armor3Weapon0", contentManager.Load<Texture2D>("WarriorSprites\\Armor3Weapon0.png"));
            textures.Add("Armor3Weapon1", contentManager.Load<Texture2D>("WarriorSprites\\Armor3Weapon1.png"));
            textures.Add("Armor3Weapon2", contentManager.Load<Texture2D>("WarriorSprites\\Armor3Weapon2.png"));
            textures.Add("Armor3Weapon3", contentManager.Load<Texture2D>("WarriorSprites\\Armor3Weapon3.png"));
            textures.Add("Armor3Weapon4", contentManager.Load<Texture2D>("WarriorSprites\\Armor3Weapon4.png"));
        }

        public override void Update(GameTime gameTime, Map map, IList<Enemy> enemies)
        {
            if (IsAttacking)
            {
                temp = new Rectangle(Rectangle.X - 48, Rectangle.Y - 48, 144, 144);
                
                if (weaponLevel == 3 || weaponLevel == 4)
                {
                    WeaponType = "sword";

                }
                else
                {
                    WeaponType = "spear";
                }

                currentAnimation = specificAnimations[WeaponType][FaceDirection];
                
                if (currentAnimation.IsFinished)
                {
                    IsAttacking = false;
                    currentAnimation.IsFinished = false;
                    PerformAttack();
                    if (WeaponType == "spear")
                    {
                        List<Enemy> returnedEnemies = GetTargets(enemies);
                        if (returnedEnemies != null && returnedEnemies.Count > 0)
                        {
                            Enemy enemy = returnedEnemies.Last();
                            enemy.Health -= Attack;
                            if (enemy.Health <= 0)
                            {
                                enemy.Kill();
                                Gold += enemy.Gold;
                            }
                        }
                    }
                    else if (WeaponType == "sword")
                    {
                        List<Enemy> returnedEnemies = GetTargets(enemies);
                        foreach (var enemy in returnedEnemies)
                        {
                            enemy.Health -= Attack / returnedEnemies.Count;
                            if (enemy.Health <= 0)
                            {
                                enemy.Kill();
                                Gold += enemy.Gold;
                            }
                        }
                    }
                    currentAnimation = movingAnimations[FaceDirection];
                }
            }
            base.Update(gameTime, map, enemies);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsAttacking)
                spriteBatch.Draw(currentTexture, this.temp, currentAnimation.drawRectangle, Color.White);
            else
                spriteBatch.Draw(currentTexture, this.Rectangle, currentAnimation.drawRectangle, Color.White);
        }

        public override void PerformAttack()
        {
            base.PerformAttack();
            
        }
    }
}
