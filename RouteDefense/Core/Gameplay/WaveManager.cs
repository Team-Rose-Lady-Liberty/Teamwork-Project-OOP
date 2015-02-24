using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        private List<Texture2D> EnemyTextures; 

        private bool canUpdate;

        public int WaveLevel { get; private set; }

        public WaveManager(Tile[] pathTile, ContentManager content)
        {
            this.enemyPath = pathTile;
            canUpdate = false;
            LoadContent(content);
            NextWave();
        }

        private void LoadContent(ContentManager content)
        {
            EnemyTextures = new List<Texture2D>();
            EnemyTextures.Add(content.Load<Texture2D>("EnemiesSprites\\EnemyBaby0.png"));
        }

        public void Start()
        {
            canUpdate = true;
        }

        public void Stop()
        {
            canUpdate = false;
        }

        public void Update(GameTime gameTime)
        {
            if(canUpdate == true)
                CurrentWave.Update(gameTime, enemyPath);
        }

        public void NextWave()
        {
            CurrentWave = new Wave(EnemyTextures[(new Random()).Next(0, EnemyTextures.Count)], 10);
            WaveLevel++;
        }

        public List<Enemy> GetSpawnedList()
        {
            return CurrentWave.GetEnemies().Where(enemy => enemy.IsAlive == true).ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
