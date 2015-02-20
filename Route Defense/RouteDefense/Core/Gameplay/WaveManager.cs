using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Models.GameObjects;
using RouteDefense.Models.GameObjects.Units;
using IDrawable = RouteDefense.Interfaces.IDrawable;
using IUpdateable = RouteDefense.Interfaces.IUpdateable;

namespace RouteDefense.Core.Gameplay
{
    public class WaveManager : IUpdateable, IDrawable
    {
        private Tile[] enemyPath;
        public Wave CurrentWave { get; private set; }

        private bool canUpdate;

        public void Start()
        {
            canUpdate = true;
        }

        public void Stop()
        {
            canUpdate = false;
        }

        public WaveManager(Tile[] pathTile)
        {
            this.enemyPath = pathTile;
            canUpdate = false;
            NextWave();
        }

        public void Update(GameTime gameTime)
        {
            if(canUpdate == true)
            CurrentWave.Update(gameTime, enemyPath);
        }

        public void NextWave()
        {
            CurrentWave = new Wave(10);
        }

        public List<Enemy> GetSpawnedList()
        {
            return CurrentWave.GetEnemies();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
