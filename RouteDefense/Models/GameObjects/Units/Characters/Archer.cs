using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    class Archer : Character
    {
        private Texture2D arrowUp;
        private Texture2D arrowDown;

        private Texture2D arrowLeft;
        private Texture2D arrowRight;

        public List<Arrow> arrows;

        private Dictionary<string, Texture2D> textures; 

        public Archer(string id, Rectangle rectangle, ContentManager contentManager)
            : base(id, rectangle, contentManager, 64, 2, 20, 4)
        {
            arrows = new List<Arrow>();
            textures = new Dictionary<string, Texture2D>();
            LoadContent(contentManager);

            currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];
 
            attackAnimations.Add(MoveDirection.Down, new Animation(0, 1152, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Up, new Animation(0, 1024, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Left, new Animation(0, 1088, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Right, new Animation(0, 1216, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
        }

        public override void LoadContent(ContentManager contentManager)
        {
            arrowUp = contentManager.Load<Texture2D>("ArcherSprites\\arrows\\arrowUp.png");
            arrowRight = contentManager.Load<Texture2D>("ArcherSprites\\arrows\\arrowRight.png");
            arrowLeft = contentManager.Load<Texture2D>("ArcherSprites\\arrows\\arrowLeft.png");
            arrowDown = contentManager.Load<Texture2D>("ArcherSprites\\arrows\\arrowDown.png");

            textures.Add("Armor0Weapon0", contentManager.Load<Texture2D>("ArcherSprites\\Armor0Weapon0.png"));
            textures.Add("Armor0Weapon1", contentManager.Load<Texture2D>("ArcherSprites\\Armor0Weapon1.png"));
            textures.Add("Armor0Weapon2", contentManager.Load<Texture2D>("ArcherSprites\\Armor0Weapon2.png"));
            textures.Add("Armor0Weapon3", contentManager.Load<Texture2D>("ArcherSprites\\Armor0Weapon3.png"));

            textures.Add("Armor1Weapon0", contentManager.Load<Texture2D>("ArcherSprites\\Armor1Weapon0.png"));
            textures.Add("Armor1Weapon1", contentManager.Load<Texture2D>("ArcherSprites\\Armor1Weapon1.png"));
            textures.Add("Armor1Weapon2", contentManager.Load<Texture2D>("ArcherSprites\\Armor1Weapon2.png"));
            textures.Add("Armor1Weapon3", contentManager.Load<Texture2D>("ArcherSprites\\Armor1Weapon3.png"));

            textures.Add("Armor2Weapon0", contentManager.Load<Texture2D>("ArcherSprites\\Armor2Weapon0.png"));
            textures.Add("Armor2Weapon1", contentManager.Load<Texture2D>("ArcherSprites\\Armor2Weapon1.png"));
            textures.Add("Armor2Weapon2", contentManager.Load<Texture2D>("ArcherSprites\\Armor2Weapon2.png"));
            textures.Add("Armor2Weapon3", contentManager.Load<Texture2D>("ArcherSprites\\Armor2Weapon3.png"));
        }

        public override void PerformAttack()
        {
            int speed = 4;
            switch (FaceDirection)
            {
                case MoveDirection.Up:
                    arrows.Add(new Arrow("temp", new Rectangle(ActualRectangle.X + 8, ActualRectangle.Y + 32, 4, 24),
                        new Point(0, -speed), arrowUp));
                    break;
                case MoveDirection.Down:
                    arrows.Add(new Arrow("temp", new Rectangle(ActualRectangle.X + 8, ActualRectangle.Y, 4, 24),
                        new Point(0, speed), arrowDown));
                    break;
                case MoveDirection.Right:
                    arrows.Add(new Arrow("temp", new Rectangle(ActualRectangle.X, ActualRectangle.Y + 28, 24, 4),
                        new Point(speed, 0), arrowRight));
                    break;
                case MoveDirection.Left:
                    arrows.Add(new Arrow("temp", new Rectangle(ActualRectangle.X + 32, ActualRectangle.Y + 28, 24, 4),
                        new Point(-speed, 0), arrowLeft));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAttacking)
            {
                currentAnimation = attackAnimations[FaceDirection];
                
                if (currentAnimation.IsFinished)
                {
                    PerformAttack();
                    IsAttacking = false;
                    currentAnimation.IsFinished = false;
                    currentAnimation = movingAnimations[FaceDirection];  
                }
            }
            foreach (var arrow in arrows)
            {
                arrow.Update();
            }
            base.Update(gameTime);
        }

        public override void UpgradeWeapon()
        {
            base.UpgradeWeapon();
            currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];
        }

        public override void UpgradeArmor()
        {
            base.UpgradeArmor();
            currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var arrow in arrows)
            {
                arrow.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}
