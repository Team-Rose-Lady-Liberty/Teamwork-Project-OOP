using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Models.GameObjects;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.Core.Gameplay
{
    public class WaveManager : IUpdateable
    {
        private Wave[] waves;
        private Tile[] enemyPath;
        public int CurrentWave { get; private set; }

        


        public WaveManager(Tile[] pathTile)
        {
            this.enemyPath = pathTile;
        }

        public void Update(GameTime gameTime)
        {
            //waves[CurrentWave].Update(gameTime, ref enemyPath);
        }

        public void NextWave()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
