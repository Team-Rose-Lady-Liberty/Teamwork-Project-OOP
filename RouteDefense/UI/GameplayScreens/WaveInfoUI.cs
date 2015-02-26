using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core.Gameplay;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.UI.GameplayScreens
{
    public class WaveInfoUI
    {
        private Label enemiesHealth;
        private Label enemiesGold;
        private Label enemyDamage;
        private Label EnemySpeed;

        public WaveInfoUI(Vector2 position)
        {
            enemiesHealth = new Label(new Rectangle(0, 575, 100, 40), "");
            enemiesGold = new Label(new Rectangle(0, 600, 100, 40), "");
            enemyDamage = new Label(new Rectangle(0, 625, 100, 40), "");
            EnemySpeed = new Label(new Rectangle(0, 650, 100, 40), "");
        }

        public void Update(Wave wave)
        {
            enemiesHealth.text = "Enemy health: " + wave.EnemyHealthMin + " - " + wave.EnemyHealthMax;
            enemiesGold.text = "Enemy gold: " + wave.GoldPerEnemyMin + " - " + wave.GoldPerEnemyMax;
            enemyDamage.text = "Enemy damage: " + wave.EnemyDamageMin + " - " + wave.EnemyDamageMax;
            EnemySpeed.text = "Enemy speed: 1";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            enemiesHealth.Draw(spriteBatch);
            enemiesGold.Draw(spriteBatch);
            EnemySpeed.Draw(spriteBatch);
            enemyDamage.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
