using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    public class Character : Unit
    {
        protected Texture2D currentTexture;

        protected Dictionary<MoveDirection, Animation> movingAnimations;

        protected Dictionary<MoveDirection, Animation> attackAnimations;

        public bool IsMoving;

        public bool IsAttacking { get; set; }

        protected int armorLevel;

        protected int weaponLevel;

        public int AttackSpeed { get; set; }

        public MoveDirection FaceDirection { get; set; }

        public Rectangle ActualRectangle;

        public Rectangle DrawRectangle;

        public MoveDirection MoveState { get; private set; }

        public Point LastPosition;

        protected Animation currentAnimation;

        public Character(string id, Rectangle rectangle,  ContentManager contentManager,
            int range, int movementSpeed, int attack, int attackSpeed)
            : base(id, rectangle)
        {
            Speed = movementSpeed;
            Range = range;
            Attack = attack;
            AttackSpeed = attackSpeed;

            weaponLevel = 0;
            armorLevel = 0;
            IsMoving = false;

            FaceDirection = MoveDirection.Down;

            movingAnimations = new Dictionary<MoveDirection, Animation>();

            movingAnimations.Add(MoveDirection.Down, new Animation(0, Constants.WalkDownStartPositionY, 
                Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));

            movingAnimations.Add(MoveDirection.Up, new Animation(0, Constants.WalkUpStartPositionY,
                Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));

            movingAnimations.Add(MoveDirection.Left, new Animation(0, Constants.WalkLeftStartPositionY, 
                Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));

            movingAnimations.Add(MoveDirection.Right, new Animation(0, Constants.WalkRightStartPositionY, 
                Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));

            attackAnimations = new Dictionary<MoveDirection, Animation>();
            
            RangeRectangle = new Rectangle(Rectangle.X - Range, Rectangle.Y - Range,
                Rectangle.Width + 2 * Range, Rectangle.Height + 2 * Range);

            ActualRectangle = new Rectangle(Rectangle.X + 8, Rectangle.Y, Rectangle.Width - 16, Rectangle.Height);

            LastPosition = new Point(ActualRectangle.X, ActualRectangle.Y);
            currentAnimation = movingAnimations[FaceDirection];
        }

        public virtual void PerformAttack()
        {
            
        }

        public void Move(MoveDirection direction)
        {
            if (IsAttacking)
            {
                currentAnimation.Reset();
                IsAttacking = false;
            }

            int speedX = 0;
            int speedY = 0;

            FaceDirection = direction;

            switch (FaceDirection)
            {
                case MoveDirection.Up:
                    speedY = -this.Speed;
                    break;
                case MoveDirection.Down:
                    speedY = this.Speed;
                    break;
                case MoveDirection.Right:
                    speedX = this.Speed;
                    break;
                case MoveDirection.Left:
                    speedX = -this.Speed;
                    break;
            }
            
            LastPosition = new Point(Rectangle.X, Rectangle.Y);
            
            Rectangle = new Rectangle(Rectangle.X + speedX, Rectangle.Y + speedY,
                Rectangle.Width, Rectangle.Height);
            
            RangeRectangle = new Rectangle(Rectangle.X - Range, Rectangle.Y - Range,
                RangeRectangle.Width, RangeRectangle.Height);
            
            ActualRectangle = new Rectangle(Rectangle.X + 16, Rectangle.Y, Rectangle.Width - 32, Rectangle.Height);

            currentAnimation = movingAnimations[FaceDirection];
        }

        public void ReturnLastPosition()
        {
            Rectangle = new Rectangle(LastPosition.X, LastPosition.Y, Rectangle.Width, Rectangle.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if(IsMoving && currentAnimation.IsLooping == false)
                currentAnimation.Start();
            if (IsMoving == false && IsAttacking == false)
            {
                currentAnimation.Stop();
            }
            
            currentAnimation.Update(gameTime);
        }

        public List<Enemy> GetTargets(IEnumerable<Enemy> enemies)
        {
            return (from enemy in enemies
                    where this.RangeRectangle.Intersects(enemy.Rectangle)
                select enemy).ToList();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            //spriteBatch.Draw(temp, this.ActualRectangle, Color.White);
            //spriteBatch.Draw(temp, this.RangeRectangle, Color.White);

            spriteBatch.Draw(currentTexture, this.Rectangle, currentAnimation.drawRectangle, Color.White);
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            
        }

        public virtual void UpgradeArmor()
        {
            this.armorLevel++;
        }

        public virtual void UpgradeWeapon()
        {
            this.weaponLevel++;
        }
    }
}
