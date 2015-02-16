using RouteDefense.Interfaces;
using RouteDefense.Models.GameObjects;

namespace RouteDefense.Core.Gameplay
{
    public class WaveManager : IUpdateable
    {
        private Wave[] waves;
        private Tile[] enemyPath;

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
    }
}
