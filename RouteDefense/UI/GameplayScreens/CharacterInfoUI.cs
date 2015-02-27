using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RouteDefense.Models.GameObjects.Units;
using RouteDefense.UI.GUIElements;

namespace RouteDefense
{
    public class CharacterInfoUI
    {
        private Label characterType;
        private Label attackDamage;
        private Label attackSpeed;
        private Label movementSpeed;
        private Label gold;

        public CharacterInfoUI()
        {
            characterType = new Label(new Rectangle(500, 535, 100, 40), "Class:");
            attackDamage = new Label(new Rectangle(500, 570, 100, 40), "Attack damage:");
            attackSpeed = new Label(new Rectangle(500, 595, 100, 40), "Attack speed:");
            movementSpeed = new Label(new Rectangle(500, 620, 100, 40), "Movement speed:");
            gold = new Label(new Rectangle(750, 510, 100, 40), "Gold:");
        }

        public void Update(Character character)
        {
            characterType.text = "Class: " + character.GetType().Name;
            attackDamage.text = "Attack damage: " + character.Attack;
            attackSpeed.text = "Attack speed: " + character.AttackSpeed;
            movementSpeed.text = "Movement speed: " + character.Speed;
            gold.text = "Gold: " + character.Gold;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            characterType.Draw(spriteBatch);
            attackDamage.Draw(spriteBatch);
            attackSpeed.Draw(spriteBatch);
            movementSpeed.Draw(spriteBatch);
            gold.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
