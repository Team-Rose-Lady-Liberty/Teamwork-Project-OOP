namespace RoseLadyLibertyOOPProject.Core
{
    using GameObjects.Map;
    using GameObjects.Units;
    using GameObjects.Units.Enemies;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;

    public class EnemyManager
    {
        private List<Enemy> enemies;
        private Tile[] enemyPath;
        public EnemyManager(Tile[] enemyPath)
        {
            enemies = new List<Enemy>();
            this.enemyPath = enemyPath;
        }

        public void Update()
        {
            for (int enemy = 0; enemy < enemies.Count; enemy++)
            {
                enemies[enemy].Update(ref enemyPath);
                if (enemies[enemy].AtFinish)
                {
                    enemies.Remove(enemies[enemy]);
                }
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void AddEnemy()
        {
            this.enemies.Add(new SampleEnemy("sd", enemyPath[0].Rectangle, 10, 10, 10));
        }
    }
}
