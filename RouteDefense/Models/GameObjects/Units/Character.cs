using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core.Gameplay;
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

        public float AttackSpeed { get; set; }

        public MoveDirection FaceDirection { get; set; }

        public Rectangle ActualRectangle;

        public Rectangle DrawRectangle;

        protected Point LastPosition;

        protected Animation currentAnimation;

        protected Dictionary<string, Texture2D> textures;

        protected int maxArmorLevel;
        protected int maxWeaponLevel;

        public int Gold;

        public bool CanAttack { get; private set; }

        private Timer timer;

        private float seconds;

        public Character(Rectangle rectangle,  ContentManager contentManager,
            int range, int movementSpeed, int attack, float attackSpeed)
            : base(rectangle)
        {
            seconds = 0;
            timer = new Timer(100);
            timer.Start();
            timer.Elapsed += timer_Elapsed;

            Speed = movementSpeed;
            Range = range;
            Attack = attack;
            AttackSpeed = attackSpeed;

            Gold = 0;

            weaponLevel = 0;
            armorLevel = 0;
            IsMoving = false;
            CanAttack = true;
            FaceDirection = MoveDirection.Down;

            textures = new Dictionary<string, Texture2D>();

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

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.seconds += 0.1f;
        }

        public virtual void PerformAttack()
        {
            seconds = 0;
            CanAttack = false;
        }

        public void Move(MoveDirection direction)
        {
            if (IsAttacking)
            {
                attackAnimations.Values.Where(anim => anim != currentAnimation).ToList().ForEach(anim => anim.Reset());
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
            
        }

        public virtual void Update(GameTime gameTime, Map map, IList<Enemy> enemies)
        {
            if (seconds > AttackSpeed)
            {
                CanAttack = true;
            }

            if (IsMoving && currentAnimation.IsLooping == false)
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
            if (Speed < 5)
            {
                Speed += 1;
            }

            if (armorLevel < maxArmorLevel)
            {
                this.armorLevel++;
                currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];
            }
        }

        public virtual void UpgradeWeapon()
        {
            if (AttackSpeed > 0.3f)
            {
                AttackSpeed -= 0.2f;
            }

            Attack += 5;

            if (weaponLevel < maxWeaponLevel)
            {
                this.weaponLevel++;
                currentTexture = textures["Armor" + armorLevel + "Weapon" + weaponLevel];
            }
        }
    }
}
