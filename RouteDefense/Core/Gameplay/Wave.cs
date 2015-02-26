using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Models.GameObjects;
using RouteDefense.Models.GameObjects.Units;

using IUpdateable = RouteDefense.Interfaces.IUpdateable;
using IDrawable = RouteDefense.Interfaces.IDrawable;

namespace RouteDefense.Core.Gameplay
{
    public class Wave : IUpdateable, IDrawable
    {
        private List<Enemy> enemies;
        private int numberEnemies;

        private int spawnedEnemiesCount;

        private int spawnRate = 70;
        private int time = 0;

        public bool IsOver;

        public int GoldPerEnemyMin { get; set; }

        public int GoldPerEnemyMax { get; set; }

        public int EnemyDamageMin { get; set; }

        public int EnemyDamageMax { get; set; }

        public int EnemyMovementMin { get; set; }

        public int EnemyMovementMax { get; set; }

        public int EnemyHealthMin { get; set; }

        public int EnemyHealthMax { get; set; }

        private Texture2D waveTexture;

        public Wave(Texture2D enemyTexture, int numEnemies)
        {
            waveTexture = enemyTexture;
            enemies = new List<Enemy>();

            this.numberEnemies = numEnemies;

            spawnedEnemiesCount = 0;

            IsOver = false;
        }

        public List<Enemy> GetEnemies()
        {
            return this.enemies;
        }

        public void Update(GameTime gameTime, Tile[] enemyPath)
        {
            if (spawnedEnemiesCount < numberEnemies)
            {
                time++;
                if (time >= spawnRate)
                {
                    enemies.Add(new Enemy("temp",
                        new Rectangle(enemyPath[0].Rectangle.X, enemyPath[0].Rectangle.Y, 32, 32), waveTexture, 20, 10, 5, 1));
                    time = 0;
                    spawnedEnemiesCount++;
                }
            }
            for (int enemy = 0; enemy < enemies.Count; enemy++)
            {
                if (enemies[enemy].IsAlive)
                    enemies[enemy].Update(gameTime, ref enemyPath);
            }
        }

        public int EnemyCount
        {
            get { return this.enemies.Count; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                if(enemy.IsAlive)
                enemy.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
