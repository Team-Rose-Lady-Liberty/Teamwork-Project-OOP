using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense
{
    public abstract class MasterState : IMasterState
    {
        public GameEngine Context { get; set; }

        public abstract IMasterState Update(GameTime gameTime);
        public abstract IMasterState HandleInput();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
