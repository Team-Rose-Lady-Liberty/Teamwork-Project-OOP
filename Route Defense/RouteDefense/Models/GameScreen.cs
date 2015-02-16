using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core;
using RouteDefense.Interfaces;

namespace RouteDefense.Models
{
    public abstract class GameScreen : IDrawable
    {
        public GameScreen()
        {
            
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update();

        public abstract void HandleInput(InputHandler inputHandler);
    }
}
