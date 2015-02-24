using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    class Warrior : Character
    {
        public Warrior(string id, Rectangle rectangle, ContentManager contentManager) 
            : base(id, rectangle, contentManager, 32, 2, 12, 2)
        {

            texture = contentManager.Load<Texture2D>("WarriorSprites\\Warrior0.png");
            attackAnimations.Add(MoveDirection.Down, new Animation(0, 384, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Up, new Animation(0, 256, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Left, new Animation(0, 320, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
            attackAnimations.Add(MoveDirection.Right, new Animation(0, 448, Constants.FrameWidth, Constants.FrameHeight, 8, 0.07f));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
