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
        protected Texture2D texture;

        protected Dictionary<MoveDirection, Animation> movingAnimations;

        protected Dictionary<MoveDirection, Animation> attackAnimations;

        public bool IsMoving;

        protected int armorLevel;

        protected int weaponLevel;

        public MoveDirection FaceDirection { get; set; }

        public Rectangle ActualRectangle;

        public MoveDirection MoveState { get; private set; }

        public CharacterState CharacterState { get; private set; }

        public int AttackSpeed { get; set; }

        public Point LastPosition;

        public int VelocityX { get; set; }

        public int VelocityY { get; set; }

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
            movingAnimations.Add(MoveDirection.Down, new Animation(0, 640, Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));
            movingAnimations.Add(MoveDirection.Up, new Animation(0, 512, Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));
            movingAnimations.Add(MoveDirection.Left, new Animation(0, 578, Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));
            movingAnimations.Add(MoveDirection.Right, new Animation(0, 704, Constants.FrameWidth, Constants.FrameHeight, 9, 0.07f));

            attackAnimations = new Dictionary<MoveDirection, Animation>();
            
            this.RangeRectangle = new Rectangle(this.Rectangle.X - Range, this.Rectangle.Y - Range,
                this.Rectangle.Width + 2 * this.Range, this.Rectangle.Height + 2 * Range);
            ActualRectangle = new Rectangle(this.Rectangle.X + 8, this.Rectangle.Y, this.Rectangle.Width - 16, this.Rectangle.Height);
            LastPosition = new Point(ActualRectangle.X, ActualRectangle.Y);
        }

        public virtual void PerformAttack()
        {
            
        }

        public void Move(MoveDirection direction)
        {
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
            this.Rectangle = new Rectangle(this.Rectangle.X + speedX, this.Rectangle.Y + speedY,
                this.Rectangle.Width, this.Rectangle.Height);
            this.RangeRectangle = new Rectangle(this.Rectangle.X - Range, this.Rectangle.Y - Range,
                this.RangeRectangle.Width, this.RangeRectangle.Height);
            ActualRectangle = new Rectangle(this.Rectangle.X + 16, this.Rectangle.Y, this.Rectangle.Width - 32, this.Rectangle.Height);
        }

        public void ReturnLastPosition()
        {
            this.Rectangle = new Rectangle(LastPosition.X, LastPosition.Y, Rectangle.Width, Rectangle.Height);
        }

        public override void Update(GameTime gameTime)
        {
            if(IsMoving == true && movingAnimations[FaceDirection].IsLooping == false)
                movingAnimations[FaceDirection].Start();
            else if(IsMoving == false)
                movingAnimations[FaceDirection].Stop();
            if (IsAttacking)
            {
                attackAnimations[FaceDirection].Update(gameTime);
                if (attackAnimations[FaceDirection].IsFinished)
                {
                    IsAttacking = false;
                    attackAnimations[FaceDirection].IsFinished = false;
                    PerformAttack();
                }
            }
            this.movingAnimations[FaceDirection].Update(gameTime);
        }

        public List<Enemy> GetTargets(IEnumerable<Enemy> enemies)
        {
            return (from enemy in enemies
                    where this.RangeRectangle.Intersects(enemy.Rectangle)
                select enemy).ToList();
        }

        public bool IsAttacking { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D temp = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            temp.SetData(new[] { Color.White });

            //spriteBatch.Draw(temp, this.ActualRectangle, Color.White);
            //spriteBatch.Draw(temp, this.RangeRectangle, Color.White);

            if(IsAttacking)
                spriteBatch.Draw(texture, this.Rectangle, this.attackAnimations[FaceDirection].drawRectangle, Color.White);
            else
                spriteBatch.Draw(texture, this.Rectangle, movingAnimations[FaceDirection].drawRectangle, Color.White);
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
