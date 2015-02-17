using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Interfaces;
using RouteDefense.Models.GameObjects;

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

        public void Update()
        {
            
        }

        public void NextWave()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
