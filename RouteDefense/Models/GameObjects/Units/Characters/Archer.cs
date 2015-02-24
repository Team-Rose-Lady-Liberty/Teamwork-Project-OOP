using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Enumerations;

namespace RouteDefense.Models.GameObjects.Units
{
    class Archer : Character
    {
        public Archer(string id, Rectangle rectangle, ContentManager contentManager)
            : base(id, rectangle, contentManager, 64, 2, 20, 4)
        {
            texture = contentManager.Load<Texture2D>("ArcherSprites\\Archer0Armor1stWeaponUpgrade.png");

            attackAnimations.Add(MoveDirection.Down, new Animation(0, 1152, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Up, new Animation(0, 1024, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Left, new Animation(0, 1088, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
            attackAnimations.Add(MoveDirection.Right, new Animation(0, 1216, Constants.FrameWidth, Constants.FrameHeight, 13, 0.1f));
        }

        
    }
}
