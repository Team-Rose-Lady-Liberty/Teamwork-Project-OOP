using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RouteDefense
{
    public interface IMasterState
    {
        GameEngine Context { get; set; }

        IMasterState Update(GameTime gameTime);
        IMasterState HandleInput();
        void Draw(SpriteBatch spriteBatch);
    }
}
