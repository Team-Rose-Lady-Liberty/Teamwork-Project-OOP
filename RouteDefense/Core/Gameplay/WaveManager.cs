using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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

        private const int timeBetweenWave = 10;

        private Timer timer;

        private int seconds;

        public WaveManager(Tile[] pathTile, ContentManager content)
        {
            seconds = 0;
            this.enemyPath = pathTile;
            canUpdate = false;
            LoadContent(content);
            WaveLevel = 1;


            CurrentWave = new Wave(EnemyTextures[(new Random()).Next(0, EnemyTextures.Count)], 10 + WaveLevel, WaveLevel);
            timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
	}

        private void LoadContent(ContentManager content)
        {
            EnemyTextures = new List<Texture2D>();
            EnemyTextures.Add(content.Load<Texture2D>("EnemiesSprites\\elf1.png"));
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            seconds += 1;
        }

        private void LoadContent(ContentManager content)
        {
            EnemyTextures = new List<Texture2D>();
            for (int i = 0; i <= 7; i++)
            {
                EnemyTextures.Add(content.Load<Texture2D>("EnemiesSprites\\Enemy" + i + ".png"));
            }    
        }

        public void Update(GameTime gameTime)
        {
            if (canUpdate == false)
            {
                if (seconds >= timeBetweenWave)
                {
                    canUpdate = true;
                    NextWave();
                    seconds = 0;
                }
            }
            else
            {
                seconds = 0;
            }
            if(canUpdate == true)
                CurrentWave.Update(gameTime, enemyPath);
            if (CurrentWave.IsOver)
            {
                canUpdate = false;
            }
        }

        public void NextWave()
        {
            if (CurrentWave.IsOver == true)
            {
                canUpdate = true;
                seconds = 0;
                WaveLevel++;
                CurrentWave = new Wave(EnemyTextures[(new Random()).Next(0, EnemyTextures.Count)], 10 + WaveLevel, WaveLevel);
            }
        }

        public List<Enemy> GetSpawnedList()
        {
            return CurrentWave.GetEnemies().Where(enemy => enemy.IsAlive == true).ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(CurrentWave != null)
                CurrentWave.Draw(spriteBatch);
        }
    }
}
