using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Core.Gameplay;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.UI.GameplayScreens
{
    public class WaveInfoUI
    {
        private Label enemyHealth;
        private Label enemyGold;
        private Label enemyDamage;
        private Label enemySpeed;

        public WaveInfoUI()
        {
            enemyHealth = new Label(new Rectangle(0, 575, 100, 40), "");
            enemyGold = new Label(new Rectangle(0, 600, 100, 40), "");
            enemyDamage = new Label(new Rectangle(0, 625, 100, 40), "");
            enemySpeed = new Label(new Rectangle(0, 650, 100, 40), "");
        }

        public void Update(Wave wave)
        {
            enemyHealth.text = "Enemy health: " + wave.EnemyHealthMin + " - " + wave.EnemyHealthMax;
            enemyGold.text = "Enemy gold: " + wave.GoldPerEnemyMin + " - " + wave.GoldPerEnemyMax;
            enemyDamage.text = "Enemy damage: " + wave.EnemyDamageMin + " - " + wave.EnemyDamageMax;
            enemySpeed.text = "Enemy speed: 1";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            enemyHealth.Draw(spriteBatch);
            enemyGold.Draw(spriteBatch);
            enemySpeed.Draw(spriteBatch);
            enemyDamage.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
