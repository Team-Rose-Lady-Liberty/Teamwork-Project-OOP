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

        private int spawnRate = 70;
        private int time = 0;

        public Wave(int numEnemies)
        {
            enemies = new List<Enemy>();
            this.numberEnemies = numEnemies;
        }

        public List<Enemy> GetEnemies()
        {
            return this.enemies;
        }

        public void Update(GameTime gameTime, Tile[] enemyPath)
        {
            time++;
            if (time >= spawnRate)
            {
                enemies.Add(new Enemy("temp", new Rectangle(enemyPath[0].Rectangle.X, enemyPath[0].Rectangle.Y, 32,32)));
                time = 0;
            }
            for (int enemy = 0; enemy < enemies.Count; enemy++)
            {
                enemies[enemy].Update(gameTime, ref enemyPath);
                if (enemies[enemy].AtFinish)
                {
                    enemies.Remove(enemies[enemy]);
                }
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
                enemy.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
