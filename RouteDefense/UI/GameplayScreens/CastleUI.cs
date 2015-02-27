using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Models.GameObjects;
using RouteDefense.UI.GUIElements;

namespace RouteDefense.UI.GameplayScreens
{
    public class CastleUI
    {
        private readonly Label castleHealthLabel;
        private readonly Label priceToUpgradeCastleLabel;
        private readonly Label castleLevelLabel;

        public CastleUI()
        {
            castleHealthLabel = new Label(new Rectangle(250, 535, 100, 40), "");
            priceToUpgradeCastleLabel = new Label(new Rectangle(250, 595, 100, 40), "");
            castleLevelLabel = new Label(new Rectangle(250, 570, 100, 40), "");
        }

        public void Upgrade(Castle castle)
        {
            castleHealthLabel.text = "Health: " + castle.castleHealth;
            castleLevelLabel.text = "Level: " + castle.Level;
            priceToUpgradeCastleLabel.text = "Gold to upgrade: " + castle.PriceToUpgrade;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            castleLevelLabel.Draw(spriteBatch);
            priceToUpgradeCastleLabel.Draw(spriteBatch);
            castleHealthLabel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
